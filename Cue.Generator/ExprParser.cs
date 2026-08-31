using System.Numerics;
using Cuelang.Cue;

namespace Cue.Generator;

public readonly record struct ExprBounds(BigInteger? Lower, BigInteger? Upper)
{
    public static ExprBounds Unknown => new(null, null);
    public static ExprBounds Exact(BigInteger value) => new(value, value);
    public static ExprBounds Range(BigInteger? lower, BigInteger? upper) => new(lower, upper);
    public bool IsKnown => Lower is not null || Upper is not null;
}

public static class NumberBoundExtensions
{
    private static readonly (BigInteger Min, BigInteger Max, Type Type)[] NetBounds =
    [
        MinMaxValue<byte>(),
        MinMaxValue<sbyte>(),
        MinMaxValue<ushort>(),
        MinMaxValue<short>(),
        MinMaxValue<char>(),
        MinMaxValue<uint>(),
        MinMaxValue<int>(),
        MinMaxValue<ulong>(),
        MinMaxValue<long>(),
        MinMaxValue<UInt128>(),
        MinMaxValue<Int128>()
    ];

    private static (BigInteger, BigInteger, Type) MinMaxValue<T>()
        where T : IMinMaxValue<T>
        => (
            BigInteger.Parse(T.MinValue.ToString()!),
            BigInteger.Parse(T.MaxValue.ToString()!),
            typeof(T));

    public static Type TypeFor(BigInteger value)
    {
        foreach (var (min, max, type) in NetBounds)
        {
            if (min <= value && value <= max)
                return type;
        }

        return typeof(BigInteger);
    }

    extension(Value value)
    {
        public BigInteger? LowerBound() => Analyze(value).Lower;
        public BigInteger? UpperBound() => Analyze(value).Upper;
        public ExprBounds Bounds() => Analyze(value);
    }

    private static ExprBounds Analyze(Value value)
    {
        var expr = value.Expr();

        return expr.Op switch
        {
            ExprOp.No => AnalyzeNo(value),

            ExprOp.Add => AnalyzeFold(expr.Values, Add),
            ExprOp.Subtract => AnalyzeSubtract(expr.Values),
            ExprOp.Multiply => AnalyzeFold(expr.Values, Multiply),
            ExprOp.FloatQuotient => ExprBounds.Unknown,

            ExprOp.BooleanAnd or
            ExprOp.BooleanOr or
            ExprOp.Not => ExprBounds.Unknown,

            ExprOp.And => AnalyzeAnd(expr.Values),
            ExprOp.Or => AnalyzeOr(expr.Values),

            ExprOp.Equal => AnalyzeComparison(expr, static x => ExprBounds.Exact(x)),
            ExprOp.NotEqual => ExprBounds.Unknown,
            ExprOp.LessThan => AnalyzeComparison(expr, static x => ExprBounds.Range(null, x - 1)),
            ExprOp.LessThanEqual => AnalyzeComparison(expr, static x => ExprBounds.Range(null, x)),
            ExprOp.GreaterThan => AnalyzeComparison(expr, static x => ExprBounds.Range(x + 1, null)),
            ExprOp.GreaterThanEqual => AnalyzeComparison(expr, static x => ExprBounds.Range(x, null)),

            ExprOp.RegexMatch or
            ExprOp.NotRegexMatch or
            ExprOp.Slice or
            ExprOp.Interpolation => ExprBounds.Unknown,

            ExprOp.Selector => AnalyzeSelector(expr),
            ExprOp.Index => AnalyzeIndex(expr),
            ExprOp.Call => AnalyzeCall(expr),

            _ => AnalyzeConcrete(value)
        };
    }

    private static ExprBounds AnalyzeNo(Value value)
    {
        return value.IsConcrete()
            ? AnalyzeConcrete(value)
            : ExprBounds.Unknown;
    }

    private static ExprBounds AnalyzeFold(Value[] values, Func<ExprBounds, ExprBounds, ExprBounds> operation)
    {
        if (values.Length == 0)
            return ExprBounds.Unknown;

        var result = Analyze(values[0]);

        for (var i = 1; i < values.Length; i++)
            result = operation(result, Analyze(values[i]));

        return result;
    }

    private static ExprBounds AnalyzeSubtract(Value[] values)
    {
        return values switch
        {
            [] => ExprBounds.Unknown,
            [{ } value] when Analyze(value) is var (lower, upper) => ExprBounds.Range(Negate(upper), Negate(lower)),
            _ => AnalyzeFold(values, Subtract)
        };
    }

    private static ExprBounds AnalyzeAnd(Value[] values) =>
        values.Aggregate(
            ExprBounds.Unknown,
            static (current, value) =>
                Intersect(current, AnalyzeConstraint(value)));

    private static ExprBounds AnalyzeOr(Value[] values) =>
        values
            .Select(Analyze)
            .Where(static x => x.IsKnown)
            .Aggregate<ExprBounds, ExprBounds?>(
                null,
                static (current, value) =>
                    current is null ? value : Union(current.Value, value))
            ?? ExprBounds.Unknown;

    private static ExprBounds AnalyzeConstraint(Value value)
    {
        var expr = value.Expr();

        return expr.Op switch
        {
            ExprOp.GreaterThan => AnalyzeComparison(expr, static x => ExprBounds.Range(x + 1, null)),
            ExprOp.GreaterThanEqual => AnalyzeComparison(expr, static x => ExprBounds.Range(x, null)),
            ExprOp.LessThan => AnalyzeComparison(expr, static x => ExprBounds.Range(null, x - 1)),
            ExprOp.LessThanEqual => AnalyzeComparison(expr, static x => ExprBounds.Range(null, x)),
            ExprOp.Equal => AnalyzeComparison(expr, static x => ExprBounds.Exact(x)),
            
            ExprOp.And => AnalyzeAnd(expr.Values),
            ExprOp.Or => AnalyzeOr(expr.Values),
            ExprOp.No => AnalyzeNo(value),

            _ => Analyze(value)
        };
    }

    private static ExprBounds AnalyzeComparison(
        ExprResult expr,
        Func<BigInteger, ExprBounds> createBounds)
    {
        return TryGetComparisonConstant(expr, out var constant)
            ? createBounds(constant)
            : ExprBounds.Unknown;
    }

    private static bool TryGetComparisonConstant(ExprResult expr, out BigInteger constant)
    {
        constant = default;
        return expr.Values.Length == 1 && TryGetInteger(expr.Values[0], out constant);
    }

    private static ExprBounds AnalyzeSelector(ExprResult expr) =>
        expr.Values.Length < 2
            ? ExprBounds.Unknown
            : Analyze(expr.Values[^1]);

    private static ExprBounds AnalyzeIndex(ExprResult expr)
    {
        if (expr.Values.Length != 2)
            return ExprBounds.Unknown;

        var collection = expr.Values[0];
        var index = expr.Values[1];

        if (!TryGetInteger(index, out var i) ||
            i < 0 ||
            i > int.MaxValue ||
            !collection.IsConcrete())
        {
            return ExprBounds.Unknown;
        }

        var indexed = TryGetIndex(collection, (int)i);
        return indexed is null ? ExprBounds.Unknown : Analyze(indexed);
    }

    private static ExprBounds AnalyzeCall(ExprResult expr) =>
        expr.CallName == "$len"
            ? ExprBounds.Range(BigInteger.Zero, null)
            : ExprBounds.Unknown;

    private static ExprBounds AnalyzeConcrete(Value value) =>
        TryGetInteger(value, out var integer)
            ? ExprBounds.Exact(integer)
            : ExprBounds.Unknown;

    private static bool TryGetInteger(Value value, out BigInteger result)
    {
        result = default;

        if (!value.IsConcrete())
            return false;

        try
        {
            return BigInteger.TryParse(value.GetJson(), out result);
        }
        catch
        {
            return false;
        }
    }

    private static ExprBounds Add(ExprBounds a, ExprBounds b) =>
        ExprBounds.Range(
            Add(a.Lower, b.Lower),
            Add(a.Upper, b.Upper));

    private static ExprBounds Subtract(ExprBounds a, ExprBounds b) =>
        ExprBounds.Range(
            Subtract(a.Lower, b.Upper),
            Subtract(a.Upper, b.Lower));

    private static ExprBounds Multiply(ExprBounds a, ExprBounds b)
    {
        if (a is not { Lower: { } aLower, Upper: { } aUpper }
            || b is not { Lower: { } bLower, Upper: { } bUpper })
        {
            return ExprBounds.Unknown;
        }

        var products = new[]
        {
            aLower * bLower, 
            aLower * bUpper,
            aUpper * bLower, 
            aUpper * bUpper
        };
        return ExprBounds.Range(products.Min(), products.Max());
    }

    private static BigInteger? Add(BigInteger? a, BigInteger? b) =>
        a is { } x && b is { } y ? x + y : null;

    private static BigInteger? Subtract(BigInteger? a, BigInteger? b) =>
        a is { } x && b is { } y ? x - y : null;

    private static BigInteger? Negate(BigInteger? value) =>
        value is { } x ? -x : null;

    private static ExprBounds Intersect(ExprBounds a, ExprBounds b)
    {
        if (!a.IsKnown)
            return b;

        if (!b.IsKnown)
            return a;

        return ExprBounds.Range(Max(a.Lower, b.Lower), Min(a.Upper, b.Upper));
    }

    private static ExprBounds Union(ExprBounds a, ExprBounds b) =>
        ExprBounds.Range(Min(a.Lower, b.Lower), Max(a.Upper, b.Upper));

    private static BigInteger? Min(BigInteger? a, BigInteger? b) =>
        a is { } aVal && b is { } bVal
            ? BigInteger.Min(aVal, bVal)
            : null;

    private static BigInteger? Max(BigInteger? a, BigInteger? b) =>
        a is { } aVal && b is { } bVal
            ? BigInteger.Max(aVal, bVal)
            : null;

    private static Value? TryGetIndex(Value value, int index)
    {
        try
        {
            return value.Lookup($"[{index}]");
        }
        catch (CueError)
        {
            return null;
        }
    }
}

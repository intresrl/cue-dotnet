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
        public BigInteger? LowerBound() => ParseAndAnalyze(value).Lower;
        public BigInteger? UpperBound() => ParseAndAnalyze(value).Upper;
        public ExprBounds Bounds() => ParseAndAnalyze(value);
    }

    private static ExprBounds ParseAndAnalyze(Value value)
    {
        var visitor = new CueExprVisitor();
        var cueExpr = visitor.Visit(value);
        return AnalyzeExpr(cueExpr);
    }

    private static ExprBounds AnalyzeExpr(CueExpr expr)
    {
        return expr switch
        {
            CueIntegerExpr integer => ExprBounds.Exact(integer.Value),
            CueFloatExpr => ExprBounds.Unknown,
            CueStringExpr => ExprBounds.Unknown,
            CueBoolExpr => ExprBounds.Unknown,
            CueBytesExpr => ExprBounds.Unknown,
            CueUnknownExpr => ExprBounds.Unknown,

            CueUnaryExpr unary => AnalyzeUnary(unary),
            CueBinaryExpr binary => AnalyzeBinary(binary),
            CueLogicalExpr logical => AnalyzeLogical(logical),

            CueSelectorExpr selector => AnalyzeExpr(selector.Target),
            CueIndexExpr index => AnalyzeExpr(index.Target),
            CueSliceExpr => ExprBounds.Unknown,
            CueCallExpr call => AnalyzeCall(call),
            CueInterpolationExpr => ExprBounds.Unknown,
            CueRegexMatchExpr => ExprBounds.Unknown,

            _ => ExprBounds.Unknown
        };
    }

    private static ExprBounds AnalyzeUnary(CueUnaryExpr unary)
    {
        var operandBounds = AnalyzeExpr(unary.Operand);

        return unary.Operator switch
        {
            CueUnaryExpr.Op.Subtract => ExprBounds.Range(Negate(operandBounds.Upper), Negate(operandBounds.Lower)),
            CueUnaryExpr.Op.Add => operandBounds,

            // Comparison constraints: >= 3, < 10, etc.
            // When these are unary, they directly represent range constraints
            CueUnaryExpr.Op.LessThan =>
                operandBounds switch
                {
                    { Lower: { } x } => ExprBounds.Range(null, x - 1),
                    { Upper: { } x } => ExprBounds.Range(null, x - 1),
                    _ => ExprBounds.Unknown
                },
            CueUnaryExpr.Op.LessThanEqual =>
                operandBounds switch
                {
                    { Lower: { } x } => ExprBounds.Range(null, x),
                    { Upper: { } x } => ExprBounds.Range(null, x),
                    _ => ExprBounds.Unknown
                },
            CueUnaryExpr.Op.GreaterThan =>
                operandBounds switch
                {
                    { Lower: { } x } => ExprBounds.Range(x + 1, null),
                    { Upper: { } x } => ExprBounds.Range(x + 1, null),
                    _ => ExprBounds.Unknown
                },
            CueUnaryExpr.Op.GreaterThanEqual =>
                operandBounds switch
                {
                    { Lower: { } x } => ExprBounds.Range(x, null),
                    { Upper: { } x } => ExprBounds.Range(x, null),
                    _ => ExprBounds.Unknown
                },

            CueUnaryExpr.Op.Equal => operandBounds,
            CueUnaryExpr.Op.NotEqual => ExprBounds.Unknown,
            CueUnaryExpr.Op.RegexMatch => ExprBounds.Unknown,
            CueUnaryExpr.Op.NotRegexMatch => ExprBounds.Unknown,
            CueUnaryExpr.Op.Not => ExprBounds.Unknown,
            _ => ExprBounds.Unknown
        };
    }

    private static ExprBounds AnalyzeBinary(CueBinaryExpr binary)
    {
        var leftBounds = AnalyzeExpr(binary.Left);
        var rightBounds = AnalyzeExpr(binary.Right);

        return binary.Operator switch
        {
            CueBinaryExpr.Op.Add => Add(leftBounds, rightBounds),
            CueBinaryExpr.Op.Subtract => Subtract(leftBounds, rightBounds),
            CueBinaryExpr.Op.Multiply => Multiply(leftBounds, rightBounds),
            CueBinaryExpr.Op.FloatQuotient => ExprBounds.Unknown,
            CueBinaryExpr.Op.BooleanAnd => ExprBounds.Unknown,
            CueBinaryExpr.Op.BooleanOr => ExprBounds.Unknown,
            CueBinaryExpr.Op.Equal => ExprBounds.Unknown,
            CueBinaryExpr.Op.NotEqual => ExprBounds.Unknown,
            CueBinaryExpr.Op.LessThan => ExprBounds.Unknown,
            CueBinaryExpr.Op.LessThanEqual => ExprBounds.Unknown,
            CueBinaryExpr.Op.GreaterThan => ExprBounds.Unknown,
            CueBinaryExpr.Op.GreaterThanEqual => ExprBounds.Unknown,
            CueBinaryExpr.Op.RegexMatch => ExprBounds.Unknown,
            CueBinaryExpr.Op.NotRegexMatch => ExprBounds.Unknown,
            _ => ExprBounds.Unknown
        };
    }

    private static ExprBounds AnalyzeLogical(CueLogicalExpr logical)
    {
        return logical.Operator switch
        {
            CueLogicalExpr.Op.And => logical.Values
                .Select(AnalyzeExpr)
                .Aggregate(
                    ExprBounds.Unknown,
                    static (current, bounds) => Intersect(current, bounds)),
            CueLogicalExpr.Op.Or => logical.Values
                .Select(AnalyzeExpr)
                .Where(static x => x.IsKnown)
                .Aggregate<ExprBounds, ExprBounds?>(
                    null,
                    static (current, bounds) =>
                        current is null ? bounds : Union(current.Value, bounds))
                ?? ExprBounds.Unknown,
            _ => ExprBounds.Unknown
        };
    }

    private static ExprBounds AnalyzeCall(CueCallExpr call)
    {
        return call.Name == "$len"
            ? ExprBounds.Range(BigInteger.Zero, null)
            : ExprBounds.Unknown;
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

}

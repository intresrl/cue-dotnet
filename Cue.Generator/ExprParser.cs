using System.Numerics;

namespace Cue.Generator;

public readonly record struct ExprBounds(BigInteger? Lower, BigInteger? Upper)
{
    public static ExprBounds Unknown => new(null, null);
    public static ExprBounds Exact(BigInteger value) => new(value, value);
    public static ExprBounds Range(BigInteger? lower, BigInteger? upper) => new(lower, upper);
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
            {
                return type;
            }
        }

        return typeof(BigInteger);
    }

    extension(CueExpr expr)
    {
        public ExprBounds Bounds() => AnalyzeExpr(expr);
    }

    private static ExprBounds AnalyzeExpr(CueExpr expr) =>
        expr switch
        {
            CueIntegerExpr integer => ExprBounds.Exact(integer.Value),
            CueAnyExpr => ExprBounds.Unknown,
            CueUnaryExpr unary => AnalyzeUnaryComparison(unary),
            CueLogicalExpr logical => AnalyzeLogical(logical),
            _ => throw new NotSupportedException($"Expression {expr} is not supported.")
        };

    private static ExprBounds AnalyzeUnaryComparison(CueUnaryExpr unary)
    {
        if (unary.Operand is not CueIntegerExpr integer)
        {
            throw new NotSupportedException(
                $"Unary comparison operand must be a concrete integer, " +
                $"but was '{unary.Operand.GetType().Name}'.");
        }

        var value = integer.Value;

        return unary.Operator switch
        {
            CueUnaryExpr.Op.LessThan => ExprBounds.Range(null, value - 1),
            CueUnaryExpr.Op.LessThanEqual => ExprBounds.Range(null, value),
            CueUnaryExpr.Op.GreaterThan => ExprBounds.Range(value + 1, null),
            CueUnaryExpr.Op.GreaterThanEqual => ExprBounds.Range(value, null),
            CueUnaryExpr.Op.Equal => ExprBounds.Exact(value),

            _ => throw new NotSupportedException(
                $"Unary operator '{unary.Operator}' is not supported.")
        };
    }

    private static ExprBounds AnalyzeLogical(CueLogicalExpr logical) =>
        logical.Operator switch
        {
            CueLogicalExpr.Op.And or CueLogicalExpr.Op.Conjunction =>
                logical.Values
                    .Select(AnalyzeExpr)
                    .Aggregate(Intersect),

            CueLogicalExpr.Op.Or or CueLogicalExpr.Op.Disjunction =>
                logical.Values
                    .Select(AnalyzeExpr)
                    .Aggregate(Union),

            _ => throw new NotSupportedException(
                $"Logical operator '{logical.Operator}' is not supported.")
        };

    private static ExprBounds Intersect(ExprBounds a, ExprBounds b) =>
        ExprBounds.Range(
            MaxLowerBound(a.Lower, b.Lower),
            MinUpperBound(a.Upper, b.Upper));

    private static ExprBounds Union(ExprBounds a, ExprBounds b) =>
        ExprBounds.Range(
            MinLowerBound(a.Lower, b.Lower),
            MaxUpperBound(a.Upper, b.Upper));

    private static BigInteger? MaxLowerBound(BigInteger? a, BigInteger? b) =>
        (a, b) switch
        {
            (null, _) => b,
            (_, null) => a,
            ({ } x, { } y) => BigInteger.Max(x, y)
        };

    private static BigInteger? MinUpperBound(BigInteger? a, BigInteger? b) =>
        (a, b) switch
        {
            (null, _) => b,
            (_, null) => a,
            ({ } x, { } y) => BigInteger.Min(x, y)
        };

    private static BigInteger? MinLowerBound(BigInteger? a, BigInteger? b) =>
        (a, b) switch
        {
            (null, _) or (_, null) => null,
            ({ } x, { } y) => BigInteger.Min(x, y)
        };

    private static BigInteger? MaxUpperBound(BigInteger? a, BigInteger? b) =>
        (a, b) switch
        {
            (null, _) or (_, null) => null,
            ({ } x, { } y) => BigInteger.Max(x, y)
        };
}

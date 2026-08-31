using System.Numerics;
using ExtendedNumerics;

namespace Cue.Generator;
#if false
public readonly record struct ExprBounds(BigInteger Lower, BigInteger Upper, bool IsUnbounded)
{
    public static ExprBounds Int(BigInteger lower, BigInteger upper) => new(lower, upper, false);
    public static ExprBounds Unbounded => new(BigInteger.Zero, BigInteger.Zero, true);
}

public abstract record CueExpr;

public sealed record CueAnyInt : CueExpr;

public sealed record CueAnyFloat : CueExpr;

public sealed record CueFloatLiteral(BigDecimal Value) : CueExpr;

public sealed record CueIntLiteral(BigInteger Value) : CueExpr;

public sealed record CueBoolLiteral(bool Value) : CueExpr;

public sealed record CueUnaryExpr(UnaryOperator Operator, CueExpr Operand) : CueExpr;

public sealed record CueBinaryExpr(CueBinOp Operator, CueExpr Left, CueExpr Right) : CueExpr;

public sealed record CueLogicalAnd(CueExpr[] Expressions) : CueExpr;

public sealed record CueLogicalOr(CueExpr[] Expressions) : CueExpr;

public enum UnaryOperator
{
    NoOp,
    Not
}

public enum CueBinOp
{
    // Number
    Equal,
    Add,
    Subtract,
    Multiply,
    FloatQuotient,
    NotEqual,
    LessThan,
    LessEqual,
    GreaterThan,
    GreaterEqual
}


public static class NumberBoundExtensions
{
    private static readonly (BigInteger, BigInteger, Type)[] Bounds =
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

    private static (T, T, Type) MinMaxValue<T>()
        where T : IMinMaxValue<T>
    {
        return (T.MinValue, T.MaxValue, typeof(T));
    }

    public static Type TypeFor(BigInteger value)
    {
        return Bounds.FirstOrDefault(
            e => e is var (min, max, _) &&
                 min <= value &&
                 max >= value).Item3
            ?? typeof(BigInteger);
    }

    public static BigInteger? LowerBound(CueExpr expr) =>
        Analyze(expr).Lower;

    public static BigInteger? UpperBound(CueExpr expr) =>
        Analyze(expr).Upper;

    private static ExprBounds Analyze(CueExpr expr)
    {
        return expr switch
        {
            CueAnyInt => ExprBounds.Unbounded,
            CueAnyFloat => ExprBounds.Unbounded,
            CueIntLiteral x => ExprBounds.Int(x.Value, x.Value),
            CueFloatLiteral => ExprBounds.Unbounded,
            CueBoolLiteral => ExprBounds.Unbounded,
            CueUnaryExpr x => AnalyzeUnary(x),

            CueBinaryExpr x =>
                AnalyzeBinary(x),

            CueLogicalAnd =>
                ExprBounds.Bool,

            CueLogicalOr =>
                ExprBounds.Bool,

            _ => throw new ArgumentOutOfRangeException(
                nameof(expr),
                expr,
                "Unsupported expression.")
        };
    }

    private static ExprBounds AnalyzeUnary(CueUnaryExpr expr)
    {
        return expr.Operator switch
        {
            UnaryOperator.NoOp => Analyze(expr.Operand),
            UnaryOperator.Not => ExprBounds.Unbounded, // TODO: fix

            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private static ExprBounds AnalyzeBinary(CueBinaryExpr expr)
    {
        var left = Analyze(expr.Left);
        var right = Analyze(expr.Right);

        // All comparisons produce bool.
        switch (expr.Operator)
        {
            case CueBinOp.Equal:
            case CueBinOp.NotEqual:
            case CueBinOp.LessThan:
            case CueBinOp.LessEqual:
            case CueBinOp.GreaterThan:
            case CueBinOp.GreaterEqual:
                return ExprBounds.Unbounded;
        }

        // Float arithmetic remains float. We don't need to calculate
        // its numerical lower/upper bound.
        if (expr.Operator == CueBinOp.FloatQuotient)
        {
            return ExprBounds.Unbounded;
        }

        return expr.Operator switch
        {
            CueBinOp.Add => Add(left, right),
            CueBinOp.Subtract => Subtract(left, right),
            CueBinOp.Multiply => Multiply(left, right),

            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private static ExprBounds Add(
        ExprBounds a,
        ExprBounds b)
    {
        return ExprBounds.Int(
            a.Lower is { } al && b.Lower is { } bl
                ? al + bl
                : null,
            a.Upper is { } au && b.Upper is { } bu
                ? au + bu
                : null);
    }

    private static ExprBounds Subtract(
        ExprBounds a,
        ExprBounds b)
    {
        return ExprBounds.Int(
            a.Lower is { } al && b.Upper is { } bu
                ? al - bu
                : null,
            a.Upper is { } au && b.Lower is { } bl
                ? au - bl
                : null);
    }

    private static ExprBounds Multiply(
        ExprBounds a,
        ExprBounds b)
    {
        // If either side has no bound, we can still derive some useful
        // bounds for multiplication in special cases, but don't attempt
        // symbolic sign analysis here. Return an unbounded result.
        if (a.Lower is null || a.Upper is null ||
            b.Lower is null || b.Upper is null)
        {
            return ExprBounds.AnyInt;
        }

        var al = a.Lower.Value;
        var au = a.Upper.Value;
        var bl = b.Lower.Value;
        var bu = b.Upper.Value;

        var p1 = al * bl;
        var p2 = al * bu;
        var p3 = au * bl;
        var p4 = au * bu;

        return ExprBounds.Int(
            BigInteger.Min(
                BigInteger.Min(p1, p2),
                BigInteger.Min(p3, p4)),
            BigInteger.Max(
                BigInteger.Max(p1, p2),
                BigInteger.Max(p3, p4)));
    }
}

#endif

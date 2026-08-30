using System.Numerics;
using Cuelang.Cue;
using ExtendedNumerics;

namespace Cue.Generator;

public abstract record CueExpr;

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

    private static (T, T, Type) MinMaxValue<T>() where T : IMinMaxValue<T>
    {
        return (T.MinValue, T.MaxValue, typeof(T));
    }

    public static Type TypeFor(BigInteger value)
    {
        return Bounds.FirstOrDefault(e => e is var (min, max, _) && min <= value && max >= value).Item3 ??
               typeof(BigInteger);
    }
}

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

public static class ExprParser
{
    private static CueExpr Parse(Value value)
    {
        var expr = value.Expr();

        // NoOp is a transparent container. Its contents must be parsed as a leaf,
        // not recursively as another expression.
        if (expr.Op == ExprOp.No) return ParseLeaf(value);

        var values = expr.Values.Select(Parse).ToArray();

        return expr.Op switch
        {
            // Arithmetic
            ExprOp.Add => new CueBinaryExpr(CueBinOp.Add, values[0], values[1]), // Number, string, bytes
            ExprOp.Subtract => new CueBinaryExpr(CueBinOp.Subtract, values[0], values[1]), // Number, string, bytes
            ExprOp.Multiply => new CueBinaryExpr(CueBinOp.Multiply, values[0], values[1]),
            ExprOp.FloatQuotient => new CueBinaryExpr(CueBinOp.FloatQuotient, values[0], values[1]),

            // Comparison
            ExprOp.Equal => new CueBinaryExpr(CueBinOp.Equal, values[0], values[1]), // Number, string, bytes
            ExprOp.NotEqual => new CueBinaryExpr(CueBinOp.NotEqual, values[0], values[1]),
            ExprOp.LessThan => new CueBinaryExpr(CueBinOp.LessThan, values[0], values[1]),
            ExprOp.LessThanEqual => new CueBinaryExpr(CueBinOp.LessEqual, values[0], values[1]),
            ExprOp.GreaterThan => new CueBinaryExpr(CueBinOp.GreaterThan, values[0], values[1]),
            ExprOp.GreaterThanEqual => new CueBinaryExpr(CueBinOp.GreaterEqual, values[0], values[1]),

            // Logic
            ExprOp.BooleanAnd => new CueLogicalAnd(values),
            ExprOp.BooleanOr => new CueLogicalOr(values),
            ExprOp.Not => new CueUnaryExpr(UnaryOperator.Not, values[0]),

            _ => throw new ArgumentException(
                $"Expression operator '{expr.Op}' is not supported by the number expression parser.",
                nameof(value))
        };
    }

    private static CueExpr ParseLeaf(Value value)
    {
        var k = value.IncompleteKind();
        var isFloat = k switch
        {
            Kind.Float or Kind.Number => true,
            Kind.Int => false,
            _ => throw new ArgumentException($"Cannot parse leaf expression with kind {k}")
        };

        if (!value.IsConcrete()) return isFloat ? new CueAnyFloat() : new CueAnyInt();

        var (mantissa, exponent) = value.GetFloat();

        if (exponent is < int.MinValue or > int.MaxValue)
            throw new InvalidOperationException("exponent value too high");

        return isFloat
            ? new CueFloatLiteral(new BigDecimal(mantissa, (int)exponent))
            : new CueIntLiteral(exponent * BigInteger.Pow(2, (int)exponent));
    }
}
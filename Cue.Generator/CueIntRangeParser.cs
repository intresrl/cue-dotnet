using Cuelang.Cue;

namespace Cue.Generator;

using ExtendedNumerics;

public abstract record CueExpr;

public enum CueBound
{
    Uint,    // >=0
    Uint8,   // >=0 & <=255
    Int8,    // >=-128 & <=127
    Uint16,  // >=0 & <=65535
    Int16,   // >=-32_768 & <=32_767
    Rune,    // >=0 & <=0x10FFFF
    Uint32,  // >=0 & <=4_294_967_295
    Int32,   // >=-2_147_483_648 & <=2_147_483_647
    Uint64,  // >=0 & <=18_446_744_073_709_551_615
    Int64,   // >=-9_223_372_036_854_775_808 & <=9_223_372_036_854_775_807
    Int128,  // >=-170_141_183_460_469_231_731_687_303_715_884_105_728 & <=170_141_183_460_469_231_731_687_303_715_884_105_727
    Uint128, // >=0 & <=340_282_366_920_938_463_463_374_607_431_768_211_455
    Float
}

public sealed record PredefinedBound(CueBound Bound) : CueExpr
{
    public Kind CueKind => Bound == CueBound.Float ? Kind.Float : Kind.Int;
}

public sealed record NumberLiteral(BigDecimal Value) : CueExpr;

public sealed record BoolLiteral(bool Value) : CueExpr;

public sealed record CueUnaryExpr(UnaryOperator Operator, CueExpr Operand) : CueExpr;

public sealed record CueBinaryExpr(CueBinOp Operator, CueExpr Left, CueExpr Right) : CueExpr;

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
    GreaterEqual,

    // Integer
    IntQuotient,
    IntRemainder,
    IntDivide,
    IntModulo,

    // Boolean
    BoolAnd,
    BoolOr
}

public static class ExprParser
{
    private static CueExpr Parse(Value value)
    {
        var expr = value.Expr();

        // NoOp is a transparent container. Its contents must be parsed as a leaf,
        // not recursively as another expression.
        if (expr.Op == ExprOp.No)
        {
            return ParseLeaf(value);
        }

        var values = expr.Values.Select(Parse).ToArray();

        return expr.Op switch
        {
            // Number, string, bytes
            ExprOp.Equal => new CueBinaryExpr(CueBinOp.Equal, values[0], values[1]),
            ExprOp.Add => new CueBinaryExpr(CueBinOp.Add, values[0], values[1]),
            ExprOp.Subtract => new CueBinaryExpr(CueBinOp.Subtract, values[0], values[1]),

            // Number
            ExprOp.Multiply => new CueBinaryExpr(CueBinOp.Multiply, values[0], values[1]),
            ExprOp.FloatQuotient => new CueBinaryExpr(CueBinOp.FloatQuotient, values[0], values[1]),
            ExprOp.NotEqual => new CueBinaryExpr(CueBinOp.NotEqual, values[0], values[1]),
            ExprOp.LessThan => new CueBinaryExpr(CueBinOp.LessThan, values[0], values[1]),
            ExprOp.LessThanEqual => new CueBinaryExpr(CueBinOp.LessEqual, values[0], values[1]),
            ExprOp.GreaterThan => new CueBinaryExpr(CueBinOp.GreaterThan, values[0], values[1]),
            ExprOp.GreaterThanEqual => new CueBinaryExpr(CueBinOp.GreaterEqual, values[0], values[1]),

            // Boolean
            ExprOp.BooleanAnd => new CueBinaryExpr(CueBinOp.BoolAnd, values[0], values[1]),
            ExprOp.BooleanOr => new CueBinaryExpr(CueBinOp.BoolOr, values[0], values[1]),
            ExprOp.Not => new CueUnaryExpr(UnaryOperator.Not, values[0]),

            _ => throw new ArgumentException(
                $"Expression operator '{expr.Op}' is not supported by the number expression parser.",
                nameof(value))
        };
    }

    private static CueExpr ParseLeaf(Value value)
    {
        if (value.IncompleteKind() is not (Kind.Int or Kind.Float or Kind.Number) ||
            !value.IsConcrete())
            return new And(All());

        return new NumberLiteral(value.GetDouble());
    }
}
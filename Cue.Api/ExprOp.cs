namespace Cuelang.Cue;

public enum ExprOp
{
    No,
    And,
    Or,
    Selector,
    Index,
    Slice,
    Call,
    BooleanAnd,
    BooleanOr,
    Equal,
    Not,
    NotEqual,
    LessThan,
    LessThanEqual,
    GreaterThan,
    GreaterThanEqual,
    RegexMatch,
    NotRegexMatch,
    Add,
    Subtract,
    Multiply,
    FloatQuotient,
    Interpolation,
    Spread,
}

public sealed record ExprResult(ExprOp Op, string? CallName, Value[] Values);

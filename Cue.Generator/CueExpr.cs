using System.Numerics;
using ExtendedNumerics;

namespace Cue.Generator;

public abstract record CueExpr;

public sealed record CueUnknownExpr : CueExpr;

public sealed record CueAnyExpr(CueAnyExpr.AnyType Type) : CueExpr
{
    public enum AnyType
    {
        Integer,
        Float,
        String,
        Bool,
        Bytes
    }
}

public sealed record CueIntegerExpr(BigInteger Value) : CueExpr;

public sealed record CueFloatExpr(BigDecimal Value) : CueExpr;

public sealed record CueStringExpr(string Value) : CueExpr;

public sealed record CueBoolExpr(bool Value) : CueExpr;

public sealed record CueBytesExpr(byte[] Value) : CueExpr;

public sealed record CueUnaryExpr(CueUnaryExpr.Op Operator, CueExpr Operand) : CueExpr
{
    public enum Op
    {
        Add,
        Subtract,
        Equal,
        NotEqual,
        LessThan,
        LessThanEqual,
        GreaterThan,
        GreaterThanEqual,
        RegexMatch,
        NotRegexMatch,
        Not
    }
}

public sealed record CueBinaryExpr(CueBinaryExpr.Op Operator, CueExpr Left, CueExpr Right) : CueExpr
{
    public enum Op
    {
        Add,
        Subtract,
        Multiply,
        FloatQuotient,
        BooleanAnd,
        BooleanOr,
        Equal,
        NotEqual,
        LessThan,
        LessThanEqual,
        GreaterThan,
        GreaterThanEqual,
        RegexMatch,
        NotRegexMatch
    }
}

public sealed record CueLogicalExpr : CueExpr
{
    public enum Op
    {
        And,
        Or,
        Conjunction,
        Disjunction
    }

    public CueLogicalExpr(Op Operator, IReadOnlyList<CueExpr> Values)
    {
        if (Values.Count < 2)
        {
            throw new ArgumentException("Logical expressions require at least two operands.", nameof(Values));
        }

        this.Operator = Operator;
        this.Values = Values;
    }

    public Op Operator { get; }
    public IReadOnlyList<CueExpr> Values { get; }

    public void Deconstruct(out Op op, out IReadOnlyList<CueExpr> values)
    {
        op = Operator;
        values = Values;
    }
}

public sealed record CueSelectorExpr(CueExpr Target, string Field, string Path) : CueExpr;

public sealed record CueIndexExpr(CueExpr Target, CueExpr Index) : CueExpr;

public sealed record CueSliceExpr(CueExpr Target, CueExpr? Start, CueExpr? End) : CueExpr;

public sealed record CueCallExpr(string Name, IReadOnlyList<CueExpr> Arguments) : CueExpr;

public sealed record CueInterpolationExpr(IReadOnlyList<CueExpr> Values) : CueExpr;

public sealed record CueRegexMatchExpr(string Pattern, bool ShouldMatch) : CueExpr;

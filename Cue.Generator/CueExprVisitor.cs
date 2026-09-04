using Cuelang.Cue;
using System.Numerics;
using ExtendedNumerics;
using BinaryOp = Cue.Generator.CueBinaryExpr.Op;
using UnaryOp = Cue.Generator.CueUnaryExpr.Op;
using LogicOp = Cue.Generator.CueLogicalExpr.Op;
using static Cue.Generator.CueAnyExpr;
using System.Text.Json;

namespace Cue.Generator;

public static class BigIntegerExtensions
{
    /// <summary>
    /// BigInteger.Pow variant that supports negative exponents
    /// </summary>
    /// <param name="mantissa">The number to raise to the exponent power.</param>
    /// <param name="exponent">The result of raising value to the exponent power.</param>
    /// <exception cref="InvalidDataException">resulting value is not whole, or exponent is not an int32</exception>
    public static BigInteger Pow(this BigInteger mantissa, long exponent)
    {
        if (mantissa == BigInteger.Zero)
        {
            return BigInteger.Zero;
        }

        switch (exponent)
        {
            case > int.MaxValue or < int.MinValue:
                throw new InvalidDataException("exponent is not an int32");
            case >= 0:
                return BigInteger.Pow(mantissa, (int) exponent);
        }

        var divisor = BigInteger.One << (int)-exponent;
        var (quotient, remainder) = BigInteger.DivRem(mantissa, divisor);
        return remainder == BigInteger.Zero
            ? quotient
            : throw new InvalidDataException("CUE integer has a fractional binary representation.");
    }
}

public class CueExprVisitor
{
    private static string FormatExpr(Value value)
    {
        var expr = value.Expr();

        var call = expr.Op == ExprOp.Call ? $"Call<${expr.CallName}>" : expr.Op.ToString();

        if (expr.Op is ExprOp.No)
        {
            if (value.IsConcrete())
            {
                try
                {
                    return value.GetJson();
                }
                catch
                {
                    return $"???<{value.Path()}: {value.Kind()}>";
                }
            }

            return $"(No ???<{expr.Values[0].Path()}: {expr.Values[0].IncompleteKind()}>)";
        }

        return $"({call} {string.Join(" ", expr.Values.Select(FormatExpr))})";
    }

    public CueExpr Visit(Value value)
    {
        var exprResult = value.Expr();
        Console.WriteLine("visiting: " + FormatExpr(value));
        return VisitExpr(value, exprResult);
    }

    private List<CueExpr> VisitValues(ExprResult exprResult)
    {
        return [.. exprResult.Values.Select(Visit)];
    }

    private CueExpr VisitExpr(Value value, ExprResult expr)
    {
        var maybeResult = expr.Op switch
        {
            ExprOp.No => VisitNo(value),
            ExprOp.BooleanAnd => VisitLogicalOp(LogicOp.And, expr),
            ExprOp.BooleanOr => VisitLogicalOp(LogicOp.Or, expr),
            ExprOp.And => VisitLogicalOp(LogicOp.Conjunction, expr),
            ExprOp.Or => VisitLogicalOp(LogicOp.Disjunction, expr),
            ExprOp.Selector => VisitSelector(expr),
            ExprOp.Slice => VisitSlice(expr),
            ExprOp.Call => VisitCall(expr),
            ExprOp.Interpolation => new CueInterpolationExpr(VisitValues(expr)),
            _ => null
        };

        if (maybeResult != null)
        {
            return maybeResult;
        }

        // Check arity first to determine if operators are unary or binary
        if (expr.Values is [{ } v])
        {
            var op = expr.Op switch
            {
                ExprOp.Add => UnaryOp.Add,
                ExprOp.Subtract => UnaryOp.Subtract,
                ExprOp.Equal => UnaryOp.Equal,
                ExprOp.NotEqual => UnaryOp.NotEqual,
                ExprOp.LessThan => UnaryOp.LessThan,
                ExprOp.LessThanEqual => UnaryOp.LessThanEqual,
                ExprOp.GreaterThan => UnaryOp.GreaterThan,
                ExprOp.GreaterThanEqual => UnaryOp.GreaterThanEqual,
                ExprOp.RegexMatch => UnaryOp.RegexMatch,
                ExprOp.NotRegexMatch => UnaryOp.NotRegexMatch,
                ExprOp.Not => UnaryOp.Not,

                _ => throw new InvalidOperationException($"node at path {value.Path()} has not recognized unary expression type: {expr.Op}"),
            };

            return new CueUnaryExpr(op, Visit(v));
        }

        if (expr.Values is [{ } left, { } right])
        {
            if (expr.Op is ExprOp.Index)
            {
                return new CueIndexExpr(Visit(left), Visit(right));
            }

            var op = expr.Op switch
            {
                ExprOp.Add => BinaryOp.Add,
                ExprOp.Subtract => BinaryOp.Subtract,
                ExprOp.Multiply => BinaryOp.Multiply,
                ExprOp.FloatQuotient => BinaryOp.FloatQuotient,

                // Binary comparison operators (result in boolean - don't contribute to bounds)
                ExprOp.Equal => BinaryOp.Equal,
                ExprOp.NotEqual => BinaryOp.NotEqual,
                ExprOp.LessThan => BinaryOp.LessThan,
                ExprOp.LessThanEqual => BinaryOp.LessThanEqual,
                ExprOp.GreaterThan => BinaryOp.GreaterThan,
                ExprOp.GreaterThanEqual => BinaryOp.GreaterThanEqual,

                // regex comparisons against a constant or a selector, resolve to boolean
                ExprOp.RegexMatch => BinaryOp.RegexMatch,
                ExprOp.NotRegexMatch => BinaryOp.NotRegexMatch,

                _ => throw new InvalidOperationException($"node at path {value.Path()} has not recognized binary expression type: {expr.Op}"),
            };

            return new CueBinaryExpr(op, Visit(left), Visit(right));
        }

        throw new InvalidOperationException($"node at path {value.Path()} has not recognized {expr.Values.Length}-arity expression type: {expr.Op}");
    }

    private static CueExpr VisitNo(Value value)
    {
        if (!value.IsConcrete())
        {
            AnyType? anyType = value.IncompleteKind() switch
            {
                Kind.Int => AnyType.Integer,
                Kind.Float => AnyType.Float,
                Kind.String => AnyType.String,
                Kind.Bool => AnyType.Bool,
                Kind.Bytes => AnyType.Bytes,
                _ => null
            };

            return anyType != null
                ? new CueAnyExpr(anyType.Value)
                : new CueUnknownExpr();
        }

        return value.Kind() switch
        {
            Kind.Int when value.GetFloat() is var (m, exp) => new CueIntegerExpr(m.Pow(exp)),
            Kind.Float when value.GetFloat() is var (m, exp) => new CueFloatExpr(new BigDecimal(m, (int)exp)),
            Kind.String => new CueStringExpr(JsonSerializer.Deserialize<string>(value.GetJson())!),
            Kind.Bool => new CueBoolExpr(value.GetJson() == "true"),
            Kind.Bytes => new CueBytesExpr(value.GetBytes()),
            _ => new CueUnknownExpr()
        };
    }

    private CueLogicalExpr VisitLogicalOp(LogicOp op, ExprResult exprResult)
    {
        if (exprResult.Values.Length < 2)
        {
            throw new ArgumentException("logical operator should have 2 or more results");
        }

        return new CueLogicalExpr(op, VisitValues(exprResult));
    }

    private CueExpr VisitSelector(ExprResult expr)
    {
        if (expr.Values.Length < 2)
        {
            return new CueUnknownExpr();
        }

        var target = Visit(expr.Values[0]);
        var lastValue = expr.Values[^1];

        var field = GetSelectorField(lastValue);

        var path = string.Join(".", expr.Values.Skip(1).Select(GetSelectorField));

        return new CueSelectorExpr(target, field, path);
    }

    private CueExpr VisitSlice(ExprResult expr)
    {
        if (expr.Values.Length is not (2 or 3))
            return new CueUnknownExpr();

        var target = Visit(expr.Values[0]);
        var start = expr.Values.Length > 1 ? Visit(expr.Values[1]) : null; // TODO: verify how end only works
        var end = expr.Values.Length > 2 ? Visit(expr.Values[2]) : null;

        return new CueSliceExpr(target, start, end);
    }

    private CueCallExpr VisitCall(ExprResult expr)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(expr.CallName);
        return new CueCallExpr(expr.CallName, VisitValues(expr));
    }

    private static string GetSelectorField(Value value)
    {
        var v = value.IsConcrete() ? JsonSerializer.Deserialize<string>(value.GetJson()) : null;
        return v ?? throw new InvalidOperationException("selector must contain indices that are strings");
    }
}

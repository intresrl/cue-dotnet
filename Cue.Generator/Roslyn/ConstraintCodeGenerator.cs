using System.Numerics;
using ExtendedNumerics;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using static Microsoft.CodeAnalysis.CSharp.SyntaxKind;
using UnaryOp = Cue.Generator.CueUnaryExpr.Op;
using BinaryOp = Cue.Generator.CueBinaryExpr.Op;
using LogicalOp = Cue.Generator.CueLogicalExpr.Op;

namespace Cue.Generator.Roslyn;

public static class ConstraintCodeGenerator
{
    public static ExpressionSyntax GenerateValidationExpression(CueExpr? constraint, string paramName, string valueType)
    {
        return constraint == null
            ? LiteralExpression(TrueLiteralExpression)
            : GenerateExpression(constraint, paramName, valueType);
    }

    private static ExpressionSyntax GenerateExpression(CueExpr expr, string paramName, string valueType)
    {
        return expr switch
        {
            CueUnaryExpr unary => GenerateUnaryExpression(unary, paramName, valueType),
            CueBinaryExpr binary => GenerateBinaryExpression(binary, paramName, valueType),
            CueLogicalExpr logical => GenerateLogicalExpression(logical, paramName, valueType),
            CueIntegerExpr integer => BinaryExpression(EqualsExpression, IdentifierName(paramName), CreateNumericLiteral(integer.Value, valueType)),
            CueFloatExpr floatVal => BinaryExpression(EqualsExpression, IdentifierName(paramName), CreateDecimalLiteral(floatVal.Value, valueType)),
            CueStringExpr str => BinaryExpression(EqualsExpression, IdentifierName(paramName), LiteralExpression(StringLiteralExpression, Literal(str.Value))),
            CueBoolExpr { Value: true } => BinaryExpression(EqualsExpression, IdentifierName(paramName), LiteralExpression(TrueLiteralExpression)),
            CueBoolExpr { Value: false } => BinaryExpression(NotEqualsExpression, IdentifierName(paramName), LiteralExpression(FalseLiteralExpression)),
            _ => LiteralExpression(TrueLiteralExpression)
        };
    }

    private static ExpressionSyntax GenerateUnaryExpression(CueUnaryExpr expr, string paramName, string valueType)
    {
        if (expr.Operator == UnaryOp.Not)
        {
            return PrefixUnaryExpression(LogicalNotExpression, GenerateExpression(expr.Operand, paramName, valueType));
        }

        if (expr.Operator is UnaryOp.RegexMatch or UnaryOp.NotRegexMatch)
        {
            return GenerateRegexMatchExpression(expr.Operand, paramName, expr.Operator == UnaryOp.NotRegexMatch);
        }

        var syntaxKind = expr.Operator switch
        {
            UnaryOp.GreaterThan => GreaterThanExpression,
            UnaryOp.GreaterThanEqual => GreaterThanOrEqualExpression,
            UnaryOp.LessThan => LessThanExpression,
            UnaryOp.LessThanEqual => LessThanOrEqualExpression,
            UnaryOp.Equal => EqualsExpression,
            UnaryOp.NotEqual => NotEqualsExpression,
            _ => (SyntaxKind?)null
        };

        return syntaxKind is { } k
            ? BinaryExpression(k, IdentifierName(paramName), GenerateValueExpression(expr.Operand, valueType))
            : GenerateExpression(expr.Operand, paramName, valueType);
    }

    private static ExpressionSyntax GenerateValueExpression(CueExpr expr, string valueType)
    {
        return expr switch
        {
            CueIntegerExpr integer => CreateNumericLiteral(integer.Value, valueType),
            CueFloatExpr floatVal => CreateDecimalLiteral(floatVal.Value, valueType),
            CueStringExpr str => LiteralExpression(StringLiteralExpression, Literal(str.Value)),
            CueBoolExpr boolVal => LiteralExpression(boolVal.Value ? TrueLiteralExpression : FalseLiteralExpression),
            CueUnaryExpr { Operator: UnaryOp.Minus } unary => GenerateNegatedExpression(GenerateValueExpression(unary.Operand, valueType)),
            _ => LiteralExpression(NumericLiteralExpression, Literal(0))
        };
    }

    /// <summary>
    /// Renders an integer literal as the given CLR <paramref name="valueType"/> so the generated
    /// comparison compiles and the value is preserved exactly, regardless of magnitude:
    /// values that fit in <see cref="long"/>/<see cref="ulong"/> are emitted as suffixed integer
    /// literals, and larger values are emitted as <c>BigInteger.Parse("...")</c> using the exact
    /// decimal digits (no value is ever silently truncated).
    /// </summary>
    private static ExpressionSyntax CreateNumericLiteral(BigInteger value, string valueType)
    {
        if (valueType is "BigInteger" && (value < long.MinValue || value > long.MaxValue))
        {
            // Outside long range: parse from the exact decimal string so no precision is lost,
            // e.g. `BigInteger.Parse("123456789012345678901234567890")`.
            return InvocationExpression(
                ParseName("System.Numerics.BigInteger.Parse"),
                ArgumentList(SingletonSeparatedList(
                    Argument(LiteralExpression(StringLiteralExpression, Literal(value.ToString()))))));
        }

        // Within long/ulong range: an ordinary suffixed integer literal round-trips exactly and
        // implicitly converts to any narrower integral CLR type (byte, short, int, etc.).
        return value > long.MaxValue
            ? LiteralExpression(NumericLiteralExpression, Literal((ulong)value))
            : LiteralExpression(NumericLiteralExpression, Literal((long)value));
    }

    /// <summary>
    /// Renders a float/number literal as the given CLR <paramref name="valueType"/> so the
    /// generated comparison compiles (e.g. a `double` field can't be compared against a
    /// `decimal` literal) and the value is preserved exactly wherever the target type allows it:
    /// values that don't fit in the target type exactly are emitted as a `.Parse("...")` call
    /// using the exact decimal digits rather than silently rounding.
    /// </summary>
    private static ExpressionSyntax CreateDecimalLiteral(BigDecimal value, string valueType)
    {
        switch (valueType)
        {
            case "float":
                return LiteralExpression(NumericLiteralExpression, Literal((float)value));

            case "double":
                return LiteralExpression(NumericLiteralExpression, Literal((double)value));

            case "decimal":
                if (TryToExactDecimal(value, out var exact))
                {
                    return LiteralExpression(NumericLiteralExpression, Literal(exact));
                }

                // Outside decimal's range/precision: parse from the exact decimal string, e.g.
                // `decimal.Parse("123456789012345678901234567890.123456789")`.
                return InvocationExpression(
                    ParseName("decimal.Parse"),
                    ArgumentList(SingletonSeparatedList(
                        Argument(LiteralExpression(StringLiteralExpression, Literal(value.ToString()))))));

            default:
                // BigDecimal (or any other/unknown numeric type): parse from the exact decimal
                // string so the value is captured exactly regardless of magnitude or precision,
                // e.g. `BigDecimal.Parse("123456789012345678901234567890.123456789")`.
                return InvocationExpression(
                    ParseName("ExtendedNumerics.BigDecimal.Parse"),
                    ArgumentList(SingletonSeparatedList(
                        Argument(LiteralExpression(StringLiteralExpression, Literal(value.ToString()))))));
        }
    }

    private static bool TryToExactDecimal(BigDecimal value, out decimal exact)
    {
        try
        {
            exact = (decimal)value;
        }
        catch (OverflowException)
        {
            exact = 0;
            return false;
        }

        // decimal has less precision than BigDecimal, so round-trip the conversion to make sure
        // no digits were silently dropped before trusting it.
        return new BigDecimal(exact) == value;
    }

    private static ExpressionSyntax GenerateNegatedExpression(ExpressionSyntax expr)
    {
        return PrefixUnaryExpression(UnaryMinusExpression, expr);
    }

    private static ExpressionSyntax GenerateBinaryExpression(CueBinaryExpr expr, string paramName, string valueType)
    {
        if (expr.Operator is BinaryOp.RegexMatch or BinaryOp.NotRegexMatch)
        {
            var patternOperand = expr.Left is CueStringExpr ? expr.Left : expr.Right;
            return GenerateRegexMatchExpression(patternOperand, paramName, expr.Operator == BinaryOp.NotRegexMatch);
        }

        var left = GenerateExpression(expr.Left, paramName, valueType);
        var right = GenerateExpression(expr.Right, paramName, valueType);

        var syntaxKind = expr.Operator switch
        {
            BinaryOp.Equal => EqualsExpression,
            BinaryOp.NotEqual => NotEqualsExpression,
            BinaryOp.LessThan => LessThanExpression,
            BinaryOp.LessThanEqual => LessThanOrEqualExpression,
            BinaryOp.GreaterThan => GreaterThanExpression,
            BinaryOp.GreaterThanEqual => GreaterThanOrEqualExpression,
            _ => EqualsExpression
        };

        return BinaryExpression(syntaxKind, left, right);
    }

    private static ExpressionSyntax GenerateRegexMatchExpression(CueExpr patternExpr, string paramName, bool negate)
    {
        var patternArg = GenerateValueExpression(patternExpr, "string");

        var isMatchCall = InvocationExpression(
            ParseName("System.Text.RegularExpressions.Regex.IsMatch"),
            ArgumentList(SeparatedList([
                Argument(IdentifierName(paramName)),
                Argument(patternArg)
            ])));

        return negate
            ? PrefixUnaryExpression(LogicalNotExpression, isMatchCall)
            : isMatchCall;
    }

    private static ExpressionSyntax GenerateLogicalExpression(CueLogicalExpr expr, string paramName, string valueType)
    {
        var expressions = expr.Values.Select(v => GenerateExpression(v, paramName, valueType)).ToList();

        var syntaxKind = expr.Operator switch
        {
            LogicalOp.And => LogicalAndExpression,
            LogicalOp.Conjunction => LogicalAndExpression,
            LogicalOp.Or => LogicalOrExpression,
            LogicalOp.Disjunction => LogicalOrExpression,
            _ => LogicalAndExpression
        };

        var result = expressions[0];
        for (var i = 1; i < expressions.Count; i++)
        {
            result = BinaryExpression(syntaxKind, result, expressions[i]);
        }

        return result;
    }
}

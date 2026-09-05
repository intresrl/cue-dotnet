using System.Numerics;
using ExtendedNumerics;
using Microsoft.CodeAnalysis;
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
    public static ExpressionSyntax GenerateValidationExpression(CueExpr? constraint, string paramName, TypeSyntax valueType)
    {
        return constraint == null
            ? LiteralExpression(TrueLiteralExpression)
            : GenerateExpression(constraint, paramName, valueType);
    }

    private static ExpressionSyntax GenerateExpression(CueExpr expr, string paramName, TypeSyntax valueType)
    {
        return expr switch
        {
            CueUnaryExpr unary => GenerateUnaryExpression(unary, paramName, valueType),
            CueBinaryExpr binary => GenerateBinaryExpression(binary, paramName, valueType),
            CueLogicalExpr logical => GenerateLogicalExpression(logical, paramName, valueType),
            CueIntegerExpr integer => BinaryExpression(EqualsExpression, IdentifierName(paramName), CreateNumericLiteral(integer.Value)),
            CueFloatExpr floatVal => BinaryExpression(EqualsExpression, IdentifierName(paramName), CreateDecimalLiteral(floatVal.Value)),
            CueStringExpr str => BinaryExpression(EqualsExpression, IdentifierName(paramName), LiteralExpression(StringLiteralExpression, Literal(str.Value))),
            CueBoolExpr { Value: true } => BinaryExpression(EqualsExpression, IdentifierName(paramName), LiteralExpression(TrueLiteralExpression)),
            CueBoolExpr { Value: false } => BinaryExpression(NotEqualsExpression, IdentifierName(paramName), LiteralExpression(FalseLiteralExpression)),
            _ => LiteralExpression(TrueLiteralExpression)
        };
    }

    private static ExpressionSyntax GenerateUnaryExpression(CueUnaryExpr expr, string paramName, TypeSyntax valueType)
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
            ? BinaryExpression(k, IdentifierName(paramName), GenerateValueExpression(expr.Operand))
            : GenerateExpression(expr.Operand, paramName, valueType);
    }

    private static ExpressionSyntax GenerateValueExpression(CueExpr expr)
    {
        return expr switch
        {
            CueIntegerExpr integer => CreateNumericLiteral(integer.Value),
            CueFloatExpr floatVal => CreateDecimalLiteral(floatVal.Value),
            CueStringExpr str => LiteralExpression(StringLiteralExpression, Literal(str.Value)),
            CueBoolExpr boolVal => LiteralExpression(boolVal.Value ? TrueLiteralExpression : FalseLiteralExpression),
            CueUnaryExpr { Operator: UnaryOp.Minus } unary => GenerateNegatedExpression(GenerateValueExpression(unary.Operand)),
            _ => LiteralExpression(NumericLiteralExpression, Literal(0))
        };
    }

    private static ExpressionSyntax CreateNumericLiteral(BigInteger value)
    {
        var literalType = NumberBoundExtensions.TypeFor(value);

        if (literalType == typeof(BigInteger))
        {
            return InvocationExpression(
                ParseName("BigInteger.Parse"),
                ArgumentList(SingletonSeparatedList(
                    Argument(LiteralExpression(StringLiteralExpression, Literal(value.ToString()))))));
        }

        var literalValue = literalType switch
        {
            _ when literalType == typeof(byte) => Literal((byte)value),
            _ when literalType == typeof(sbyte) => Literal((sbyte)value),
            _ when literalType == typeof(short) => Literal((short)value),
            _ when literalType == typeof(ushort) => Literal((ushort)value),
            _ when literalType == typeof(int) => Literal((int)value),
            _ when literalType == typeof(uint) => Literal((uint)value),
            _ when literalType == typeof(long) => Literal((long)value),
            _ when literalType == typeof(ulong) => Literal((ulong)value),
            _ => throw new InvalidCastException($"Cannot convert BigInteger to {literalType}")
        };
        
        return LiteralExpression(NumericLiteralExpression, literalValue);
    }

    private static ExpressionSyntax CreateDecimalLiteral(BigDecimal value)
    {
        return DecimalValue(value) is { } exact
            ? LiteralExpression(NumericLiteralExpression, Literal(exact))
            : InvocationExpression(
                ParseName("BigDecimal.Parse"),
                ArgumentList(SingletonSeparatedList(
                    Argument(LiteralExpression(StringLiteralExpression, Literal(value.ToString()))))));
    }

    private static decimal? DecimalValue(BigDecimal value)
    {
        try
        {
            var exact = (decimal)value;
            
            // decimal has less precision than BigDecimal, so round-trip the conversion to make sure
            // no digits were silently dropped before trusting it.
            return new BigDecimal(exact) == value ? exact : null;
        }
        catch (OverflowException)
        {
            return null;
        }
    }

    private static ExpressionSyntax GenerateNegatedExpression(ExpressionSyntax expr)
    {
        return PrefixUnaryExpression(UnaryMinusExpression, expr);
    }

    private static ExpressionSyntax GenerateBinaryExpression(CueBinaryExpr expr, string paramName, TypeSyntax valueType)
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
        var patternArg = GenerateValueExpression(patternExpr);

        var isMatchCall = InvocationExpression(
            ParseName("Regex.IsMatch"),
            ArgumentList(SeparatedList([Argument(IdentifierName(paramName)), Argument(patternArg)])));

        return negate
            ? PrefixUnaryExpression(LogicalNotExpression, isMatchCall)
            : isMatchCall;
    }

    private static ExpressionSyntax GenerateLogicalExpression(CueLogicalExpr expr, string paramName, TypeSyntax valueType)
    {
        // remove neutral elements
        var neutralElement = expr.Operator is LogicalOp.And or LogicalOp.Conjunction
            ? TrueLiteralExpression
            : FalseLiteralExpression;

        var valuesNoNeutral = expr.Values
            .Select(v => GenerateExpression(v, paramName, valueType))
            .Where(v => !v.IsKind(neutralElement))
            .ToList();

        var syntaxKind = expr.Operator switch
        {
            LogicalOp.And => LogicalAndExpression,
            LogicalOp.Conjunction => LogicalAndExpression,
            LogicalOp.Or => LogicalOrExpression,
            LogicalOp.Disjunction => LogicalOrExpression,
            _ => throw new InvalidOperationException("Unknown logical operator")
        };

        return valuesNoNeutral.FirstOrDefault() is { } first
            ? valuesNoNeutral
                .Skip(1)
                .Aggregate(first, (acc, e) => BinaryExpression(syntaxKind, acc, e))
            : LiteralExpression(neutralElement);
    }
}

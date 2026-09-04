using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Numerics;
using Cuelang.Cue;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using static Microsoft.CodeAnalysis.CSharp.SyntaxKind;
using UnaryOp = Cue.Generator.CueUnaryExpr.Op;
using BinaryOp = Cue.Generator.CueBinaryExpr.Op;
using LogicalOp = Cue.Generator.CueLogicalExpr.Op;

namespace Cue.Generator.Roslyn;

public static class ConstraintCodeGenerator
{
    public static ExpressionSyntax GenerateValidationExpression(CueExpr? constraint, string paramName, Kind kind)
    {
        return constraint == null
            ? LiteralExpression(TrueLiteralExpression)
            : GenerateExpression(constraint, paramName, kind);
    }

    private static ExpressionSyntax GenerateExpression(CueExpr expr, string paramName, Kind kind)
    {
        return expr switch
        {
            CueUnaryExpr unary => GenerateUnaryExpression(unary, paramName, kind),
            CueBinaryExpr binary => GenerateBinaryExpression(binary, paramName, kind),
            CueLogicalExpr logical => GenerateLogicalExpression(logical, paramName, kind),
            CueIntegerExpr integer => BinaryExpression(EqualsExpression, IdentifierName(paramName), CreateNumericLiteral(integer.Value)),
            CueFloatExpr floatVal => BinaryExpression(EqualsExpression, IdentifierName(paramName), LiteralExpression(NumericLiteralExpression, Literal((decimal)floatVal.Value))),
            CueStringExpr str => BinaryExpression(EqualsExpression, IdentifierName(paramName), LiteralExpression(StringLiteralExpression, Literal(str.Value))),
            CueBoolExpr { Value: true } => BinaryExpression(EqualsExpression, IdentifierName(paramName), LiteralExpression(TrueLiteralExpression)),
            CueBoolExpr { Value: false } => BinaryExpression(NotEqualsExpression, IdentifierName(paramName), LiteralExpression(FalseLiteralExpression)),
            _ => LiteralExpression(TrueLiteralExpression)
        };
    }

    private static ExpressionSyntax GenerateUnaryExpression(CueUnaryExpr expr, string paramName, Kind kind)
    {
        if (expr.Operator == UnaryOp.Not)
            return PrefixUnaryExpression(LogicalNotExpression, GenerateExpression(expr.Operand, paramName, kind));

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
            : GenerateExpression(expr.Operand, paramName, kind);
    }

    private static ExpressionSyntax GenerateValueExpression(CueExpr expr)
    {
        return expr switch
        {
            CueIntegerExpr integer => CreateNumericLiteral(integer.Value),
            CueFloatExpr floatVal => LiteralExpression(NumericLiteralExpression, Literal((decimal)floatVal.Value)),
            CueStringExpr str => LiteralExpression(StringLiteralExpression, Literal(str.Value)),
            CueBoolExpr boolVal => LiteralExpression(boolVal.Value ? TrueLiteralExpression : FalseLiteralExpression),
            CueUnaryExpr { Operator: UnaryOp.Minus } unary => GenerateNegatedExpression(GenerateValueExpression(unary.Operand)),
            _ => LiteralExpression(NumericLiteralExpression, Literal(0))
        };
    }

    private static ExpressionSyntax CreateNumericLiteral(BigInteger value)
    {
        // For BigInteger, we need to construct it if it exceeds long range
        if (value >= long.MinValue && value <= long.MaxValue)
        {
            return LiteralExpression(NumericLiteralExpression, Literal((long)value));
        }

        // For values outside long range, construct as BigInteger.Parse or similar
        // For now, just use the string representation with a numeric literal
        return CastExpression(
            ParseTypeName("BigInteger"),
            LiteralExpression(NumericLiteralExpression, Literal(long.MaxValue)));
    }

    private static ExpressionSyntax GenerateNegatedExpression(ExpressionSyntax expr)
    {
        return PrefixUnaryExpression(UnaryMinusExpression, expr);
    }

    private static ExpressionSyntax GenerateBinaryExpression(CueBinaryExpr expr, string paramName, Kind kind)
    {
        var left = GenerateExpression(expr.Left, paramName, kind);
        var right = GenerateExpression(expr.Right, paramName, kind);

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

    private static ExpressionSyntax GenerateLogicalExpression(CueLogicalExpr expr, string paramName, Kind kind)
    {
        var expressions = expr.Values.Select(v => GenerateExpression(v, paramName, kind)).ToList();

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

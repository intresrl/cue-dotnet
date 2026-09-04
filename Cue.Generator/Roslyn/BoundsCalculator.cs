using System.Numerics;

using UnaryOp = Cue.Generator.CueUnaryExpr.Op;
using LogicalOp = Cue.Generator.CueLogicalExpr.Op;

namespace Cue.Generator.Roslyn;

public static class NumberBoundExtensions
{
    // Prefer signed over unsigned, larger over smaller
    private static readonly (BigInteger Min, BigInteger Max, Type Type)[] NetBounds =
    [
        MinMaxValue<byte>(),
        MinMaxValue<sbyte>(),
        MinMaxValue<ushort>(),
        MinMaxValue<short>(),
        MinMaxValue<uint>(),
        MinMaxValue<int>(),
        MinMaxValue<ulong>(),
        MinMaxValue<long>()
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

    public static string GetBoundsType(CueExpr? constraint)
    {
        var (lower, upper) = ExtractBounds(constraint);

        // If we have no bounds, default to BigInteger
        if (lower == null && upper == null)
        {
            return "BigInteger";
        }

        // Find the tightest type that can hold both bounds
        var lowerType = lower != null ? TypeFor(lower.Value) : typeof(BigInteger);
        var upperType = upper != null ? TypeFor(upper.Value) : typeof(BigInteger);

        // Use the "larger" type if they differ
        return GetLargerType(lowerType, upperType).Name;
    }

    private static (BigInteger? lower, BigInteger? upper) ExtractBounds(CueExpr? constraint)
    {
        return constraint == null 
            ? (null, null)
            : ExtractBoundsFromExpr(constraint);
    }

    private static (BigInteger? lower, BigInteger? upper) ExtractBoundsFromExpr(CueExpr expr)
    {
        return expr switch
        {
            CueLogicalExpr { Operator: LogicalOp.And or LogicalOp.Conjunction } logical => 
                CombineBounds(logical.Values.Select(ExtractBoundsFromExpr), Enumerable.Max, Enumerable.Min),
            
            CueLogicalExpr { Operator: LogicalOp.Or or LogicalOp.Disjunction } logical =>
                CombineBounds(logical.Values.Select(ExtractBoundsFromExpr), Enumerable.Min, Enumerable.Max),
            
            CueIntegerExpr integer => (integer.Value, integer.Value),
            
            CueUnaryExpr unary => ExtractBoundsFromUnary(unary),
            
            _ => (null, null)
        };
    }

    private static (BigInteger? lower, BigInteger? upper) ExtractBoundsFromUnary(CueUnaryExpr expr)
    {
        var value = ExtractNumericValue(expr.Operand);
        
        return expr.Operator switch
        {
            UnaryOp.GreaterThan => (value + 1, null),
            UnaryOp.GreaterThanEqual => (value, null),
            UnaryOp.LessThan => (null, value - 1),
            UnaryOp.LessThanEqual => (null, value),
            UnaryOp.Equal => (value, value),
            _ => (null, null)
        };
    }

    private static (BigInteger? lower, BigInteger? upper) CombineBounds(
        IEnumerable<(BigInteger?, BigInteger?)> bounds,
        Func<IEnumerable<BigInteger>, BigInteger> lowerSelector,
        Func<IEnumerable<BigInteger>, BigInteger> upperSelector)
    {
        var boundsArray = bounds.ToArray();
        var lowers = boundsArray
            .Select(x => x.Item1)
            .OfType<BigInteger>()
            .ToArray();
        var uppers = boundsArray
            .Select(x => x.Item2)
            .OfType<BigInteger>()
            .ToArray();
        
        return (
            lowers.Any() ? lowerSelector(lowers) : null, 
            uppers.Any() ? upperSelector(uppers) : null
        );
    }

    private static BigInteger? ExtractNumericValue(CueExpr expr)
    {
        return expr switch
        {
            CueIntegerExpr integer => integer.Value,
            CueUnaryExpr { Operator: UnaryOp.Minus } unary => 
                ExtractNumericValue(unary.Operand) is { } val ? -val : null,
            _ => null
        };
    }

    private static Type GetLargerType(Type t1, Type t2)
    {
        if (t1 == t2)
            return t1;

        var order = NetBounds
            .Select(e => e.Type)
            .Append(typeof(BigInteger))
            .ToArray();
        
        var idx1 = Array.IndexOf(order, t1);
        var idx2 = Array.IndexOf(order, t2);
        return idx1 > idx2 ? t1 : t2;
    }
}


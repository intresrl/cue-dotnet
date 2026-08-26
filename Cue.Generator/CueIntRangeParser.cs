using Cuelang.Cue;

namespace Cue.Generator;

public static class CueIntRangeParser
{
    public readonly record struct Range(long? Start, long? End);

    private abstract record Result;
    private sealed record Concrete(long Value) : Result;
    private sealed record Ranges(IReadOnlyList<Range> Value) : Result;

    public static IReadOnlyList<Range> ParseRange(this Value value)
    {
        return ToRanges(ParseExpression(value));
    }
    
    public static long LowerBound(this IReadOnlyList<Range> value)
    {
        return value.Count != 0 && value[0].Start is { } s 
            ? s
            : long.MinValue;
    }

    private static Result ParseExpression(Value value)
    {
        var expression = value.Expr();

        try
        {
            return expression.Op switch
            {
                ExprOp.No => ParseLeaf(value),

                ExprOp.And => new Ranges(
                    expression.Values
                        .Select(ParseExpression)
                        .Select(ToRanges)
                        .Aggregate(All(), Intersect)),

                ExprOp.Or => new Ranges(
                    Normalize(
                        expression.Values
                            .Select(ParseExpression)
                            .SelectMany(ToRanges))),

                ExprOp.LessThan => ParseComparison(expression.Values, ExprOp.LessThan),
                ExprOp.LessThanEqual => ParseComparison(expression.Values, ExprOp.LessThanEqual),
                ExprOp.GreaterThan => ParseComparison(expression.Values, ExprOp.GreaterThan),
                ExprOp.GreaterThanEqual => ParseComparison(expression.Values, ExprOp.GreaterThanEqual),

                _ => new Ranges(All())
            };
        }
        finally
        {
            foreach (var child in expression.Values) child.Dispose();
        }
    }

    private static Result ParseLeaf(Value value)
    {
        if (value.IncompleteKind() != Kind.Int || !value.IsConcrete()) return new Ranges(All());

        return new Concrete(value.GetLong());
    }

    private static Result ParseComparison(
        IReadOnlyList<Value> values,
        ExprOp op)
    {
        if (values.Count != 1) return new Ranges(All());

        var operand = ParseExpression(values[0]);

        if (operand is not Concrete concrete) return new Ranges(All());

        var value = concrete.Value;

        return op switch
        {
            ExprOp.LessThan => new Ranges([new Range(null, value)]),
            ExprOp.LessThanEqual => new Ranges([new Range(null, Increment(value))]),
            ExprOp.GreaterThan => new Ranges([new Range(Increment(value), null)]),
            ExprOp.GreaterThanEqual => new Ranges([new Range(value, null)]),
            _ => new Ranges(All())
        };
    }

    private static IReadOnlyList<Range> ToRanges(Result result)
    {
        return result switch
        {
            Concrete concrete => [new Range(concrete.Value, Increment(concrete.Value))],
            Ranges ranges => ranges.Value,

            _ => throw new InvalidOperationException()
        };
    }

    private static IReadOnlyList<Range> Intersect(IReadOnlyList<Range> left, IReadOnlyList<Range> right)
    {
        var result = new List<Range>();
        foreach (var x in left)
        {
            foreach (var y in right)
            {
                var start = MaxStart(x.Start, y.Start);
                var end = MinEnd(x.End, y.End);

                if (start is null || end is null || start < end)
                {
                    result.Add(new Range(start, end));
                }
            }
        }

        return Normalize(result);
    }

    private static IReadOnlyList<Range> Normalize(IEnumerable<Range> ranges)
    {
        var ordered = ranges
            .OrderBy(x => x.Start is null ? 0 : 1)
            .ThenBy(x => x.Start)
            .ToList();

        if (ordered.Count == 0) return [];

        var result = new List<Range> { ordered[0] };

        foreach (var current in ordered.Skip(1))
        {
            var previous = result[^1];

            if (!TouchesOrOverlaps(previous, current))
            {
                result.Add(current);
                continue;
            }

            result[^1] = previous with { End = MaxEnd(previous.End, current.End) };
        }

        return result;
    }

    private static bool TouchesOrOverlaps(Range left, Range right)
    {
        return left.End is null ||
               right.Start is null ||
               right.Start <= left.End;
    }

    private static long? MaxStart(long? left, long? right)
    {
        if (left is null) return right;
        if (right is null) return left;

        return Math.Max(left.Value, right.Value);
    }

    private static long? MinEnd(long? left, long? right)
    {
        if (left is null) return right;
        if (right is null) return left;
        
        return Math.Min(left.Value, right.Value);
    }

    private static long? MaxEnd(long? left, long? right)
    {
        if (left is null || right is null) return null;
        return Math.Max(left.Value, right.Value);
    }

    private static long? Increment(long value)
    {
        return value == long.MaxValue ? null : value + 1;
    }

    private static IReadOnlyList<Range> All()
    {
        return [new Range(null, null)];
    }
}
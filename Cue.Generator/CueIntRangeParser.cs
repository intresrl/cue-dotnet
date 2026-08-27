using System.ComponentModel;
using Cuelang.Cue;

namespace Cue.Generator;

public interface Result;
public sealed record Eq(long Value) : Result;
public sealed record NotEq(long Value) : Result;
public sealed record Gt(long Value, bool Included) : Result;
public sealed record Lt(long Value, bool Included) : Result;
public sealed record And(IEnumerable<Result> Value) : Result;
public sealed record Or(IEnumerable<Result> Value) : Result;

public readonly record struct Range : Result
{
    public bool StartIncluded { get; }
    public long? Start { get; }
    public long? End { get; }
    public bool EndIncluded { get; }

    
    private Range(bool startIncluded, long? start, long? end, bool endIncluded)
    {
        // TODO: end range cannot be specified as LongMax
        if (start > end)
        {
            throw new ArgumentException("Start must be less than end");
        }

        StartIncluded = startIncluded;
        Start = start;
        End = end;
        EndIncluded = endIncluded;
    }
    
    public static Range Closed(long start, long end) => new(true, start, end, true);
    public static Range StartOpen(long start, long end) => new(false, start, end, true);
    public static Range EndOpen(long start, long end) => new(true, start, end, false);
    public static Range Open(long start, long end) => new(false, start, end, false);
    public static Range Gte(long start) => new(true, start, null, false);
    public static Range Gt(long start) => new(false, start, null, false);
    public static Range Lt(long end) => new(false, null, end, false);
    public static Range Lte(long end) => new(false, null, end, true);
    public static readonly Range All = new(false, null, null, false);
}

public static class CueIntRangeParser
{

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

                ExprOp.And => new And(
                    expression.Values
                        .Select(ParseExpression)
                        .Select(ToRanges)
                        .Aggregate(Range.All, Intersect)),

                ExprOp.Or => new And(
                    Normalize(
                        expression.Values
                            .Select(ParseExpression)
                            .SelectMany(ToRanges))),

                ExprOp.LessThan => ParseComparison(expression.Values, ExprOp.LessThan),
                ExprOp.LessThanEqual => ParseComparison(expression.Values, ExprOp.LessThanEqual),
                ExprOp.GreaterThan => ParseComparison(expression.Values, ExprOp.GreaterThan),
                ExprOp.GreaterThanEqual => ParseComparison(expression.Values, ExprOp.GreaterThanEqual),

                _ => new And(Range.All)
            };
        }
        finally
        {
            foreach (var child in expression.Values) child.Dispose();
        }
    }

    private static Result ParseLeaf(Value value)
    {
        if (value.IncompleteKind() != Kind.Int || !value.IsConcrete()) return new And(All());

        return new Eq(value.GetLong());
    }

    private static And ParseComparison(ExprResult r)
    {
        if (r.Values is not [{ } unaryValue])
        {
            throw new ArgumentException("r is not a argument rejection");
        }

        if (ParseExpression(unaryValue) is not Eq concrete) return new And(All());

        var value = concrete.Value;

        var result = r.Op switch
        {
            ExprOp.LessThan => Range.Lt(value),
            ExprOp.LessThanEqual => Range.Lte(value),
            ExprOp.GreaterThan => Range.Gt(value),
            ExprOp.GreaterThanEqual => Range.Gte(value),
            _ => throw new ArgumentException()
        };

        return new And([result]);
    }

    private static IReadOnlyList<Range> ToRanges(Result result)
    {
        return result switch
        {
            Eq concrete => [new Range(concrete.Value, Increment(concrete.Value))],
            And ranges => ranges.Value,

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
}
using Cuelang.Cue;

namespace Cue.Generator;

public abstract class CueValueVisitor<TResult>
{
    public TResult Visit(Value value)
    {
        Console.WriteLine(value);
        // For non-concrete declarations (schemas), prefer the incomplete kind
        var kind = value.Kind() is Kind.Bottom ? value.IncompleteKind() : value.Kind();

        return Dispatch(value, kind);
    }

    protected TResult Dispatch(Value value, Kind kind)
    {
        return kind switch
        {
            // Handle Kind.Top first (could be a disjunction)
            Kind.Top => VisitTop(value),
            Kind.Struct => VisitStruct(value),
            Kind.List => VisitList(value),
            _ => VisitSimple(value, kind)
        };
    }

    protected abstract TResult VisitTop(Value value);

    protected abstract TResult VisitStruct(Value value);

    protected abstract TResult VisitList(Value value);

    protected abstract TResult VisitSimple(Value value, Kind kind);
}

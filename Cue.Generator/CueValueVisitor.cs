using Cuelang.Cue;

namespace Cue.Generator;

public abstract class CueValueVisitor<TResult>
{
    public TResult Visit(Value value)
    {
        var kind = value.Kind();
        // For non-concrete declarations (schemas), prefer the incomplete kind
        if (kind is Kind.Bottom or Kind.Top)
        {
            kind = value.IncompleteKind();
        }

        return kind switch
        {
            Kind.Struct => VisitStruct(value),
            Kind.List => VisitList(value),
            _ => VisitSimple(value, kind)
        };
    }

    protected abstract TResult VisitStruct(Value value);

    protected abstract TResult VisitList(Value value);

    protected abstract TResult VisitSimple(Value value, Kind kind);
}

using Cuelang.Cue;

namespace Cue.Generator;

public abstract class CueValueVisitor<TResult>
{
    public TResult Visit(Value value)
    {
        // For non-concrete declarations (schemas), prefer the incomplete kind
        var kind = value.Kind() is Kind.Bottom ? value.IncompleteKind() : value.Kind();
        return Dispatch(value, kind);
    }

    protected TResult Dispatch(Value value, Kind kind)
    {
        return kind switch
        {
            Kind.Bottom => VisitBottom(value),
            Kind.Null => VisitNull(value),
            Kind.Bool => VisitBool(value),
            Kind.Int => VisitInt(value),
            Kind.Float => VisitFloat(value),
            Kind.String => VisitString(value),
            Kind.Bytes => VisitBytes(value),
            Kind.Number => VisitNumber(value),
            Kind.Top => VisitTop(value),
            Kind.Struct => VisitStruct(value),
            Kind.List => VisitList(value),
            _ => throw new ArgumentOutOfRangeException(nameof(kind), kind, "Unexpected kind")
        };
    }

    protected abstract TResult VisitBottom(Value value);

    protected abstract TResult VisitNull(Value value);

    protected abstract TResult VisitBool(Value value);

    protected abstract TResult VisitInt(Value value);

    protected abstract TResult VisitFloat(Value value);

    protected abstract TResult VisitString(Value value);

    protected abstract TResult VisitBytes(Value value);

    protected abstract TResult VisitNumber(Value value);

    protected abstract TResult VisitTop(Value value);

    protected abstract TResult VisitStruct(Value value);

    protected abstract TResult VisitList(Value value);
}

using Cuelang.Cue;

namespace Cue.Generator;

public abstract class CueValueVisitor<TResult>
{
    public TResult Visit(Value value)
    {
        return value.Kind() switch
        {
            Kind.Struct => VisitStruct(value),
            Kind.List => VisitList(value),
            var kind => VisitSimple(value, kind)
        };
    }

    protected abstract TResult VisitStruct(Value value);

    protected abstract TResult VisitList(Value value);

    protected abstract TResult VisitSimple(Value value, Kind kind);
}

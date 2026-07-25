using Cuelang.Cue;

namespace Cue.Generator;

public abstract record CueValueNode(Kind Kind, string Path);

public sealed record CueStructValue(string Path, IReadOnlyList<CueStructField> Fields)
    : CueValueNode(Kind.Struct, Path);

public sealed record CueStructField(string Name, CueValueNode Value);

public sealed record CueDisjunction(string Path, IReadOnlyList<CueValueNode> Branches) : CueValueNode(Kind.Top, Path);

public sealed record CueListValue(string Path, CueValueNode ElementType)
    : CueValueNode(Kind.List, Path);

public sealed record CueSimpleValue : CueValueNode
{
    public CueSimpleValue(Kind kind, string path)
        : base(EnsureSimpleKind(kind), path)
    {
    }

    private static Kind EnsureSimpleKind(Kind kind)
    {
        return kind switch
        {
            Kind.Struct => throw new ArgumentOutOfRangeException(nameof(kind), kind, "Struct values must use CueStructValue."),
            Kind.List => throw new ArgumentOutOfRangeException(nameof(kind), kind, "List values must use CueListValue."),
            _ => kind
        };
    }
}

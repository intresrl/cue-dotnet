using Cuelang.Cue;

namespace Cue.Generator;

public abstract record CueValueNode(Kind Kind, string Path)
{
    public override string ToString() => $"{GetType().Name} at {Path}";
}

public sealed record CueTop(string Path) : CueValueNode(Kind.Top, Path)
{
    public override string ToString() => $"Top at {Path}";
}

public sealed record CueStructValue(string Path, IReadOnlyList<CueStructField> Fields)
    : CueValueNode(Kind.Struct, Path)
{
    public override string ToString()
    {
        if (Fields.Count == 0)
            return $"Struct() at {Path}";
        
        var fieldList = string.Join(", ", Fields.Select(f => $"{f.Name}: {f.Value.Kind}"));
        
        return $"Struct({fieldList}) at {Path}";
    }
}

public sealed record CueStructField(string Name, CueValueNode Value);

public sealed record CueDisjunction(
    string Path,
    IReadOnlyList<CueValueNode> Branches,
    bool IsDiscriminated)
    : CueValueNode(Kind.Top, Path)
{
    public string DiscriminatorField => "TODO remove";

    public override string ToString()
    {
        var result = $"Disjunction({Branches.Count} branches";
        result += string.Join("\n", Branches.Select(e => e.ToString()));
        result += $") at {Path}";
        return result;
    }
}

public sealed record CueListValue(string Path, CueValueNode ElementType)
    : CueValueNode(Kind.List, Path)
{
    public override string ToString() => $"List<{ElementType.Kind}> at {Path}";
}

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

    public override string ToString() => $"{Kind} at {Path}";
}


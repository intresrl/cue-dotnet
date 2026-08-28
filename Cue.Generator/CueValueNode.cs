namespace Cue.Generator;

public abstract record CueValueNode(string Path)
{
    public abstract override string ToString();
}

public sealed record CueBottomValue(string Path) : CueValueNode(Path)
{
    public override string ToString() => $"Bottom at {Path}";
}

public sealed record CueNullValue(string Path) : CueValueNode(Path)
{
    public override string ToString() => $"Null at {Path}";
}

public sealed record CueNullable(CueValueNode Value) : CueValueNode(Value.Path)
{
    public override string ToString() => $"Nullable {Value}";
}

public sealed record CueBoolValue(string Path, bool? ConcreteValue = null) : CueValueNode(Path)
{
    public override string ToString() => $"Bool at {Path}";
}

public sealed record CueIntValue(string Path, long? ConcreteValue = null) : CueValueNode(Path)
{
    public override string ToString() => $"Int at {Path}";
}

public sealed record CueFloatValue(string Path, double? ConcreteValue = null) : CueValueNode(Path)
{
    public override string ToString() => $"Float at {Path}";
}

public sealed record CueStringValue(string Path, string? ConcreteValue = null) : CueValueNode(Path)
{
    public override string ToString() => $"String at {Path}";
}

public sealed record CueBytesValue(string Path, byte[]? ConcreteValue = null) : CueValueNode(Path)
{
    public override string ToString() => $"Bytes at {Path}";
}

public sealed record CueNumberValue(string Path) : CueValueNode(Path)
{
    public override string ToString() => $"Number at {Path}";
}

public sealed record CueTopValue(string Path) : CueValueNode(Path)
{
    public override string ToString() => $"Top at {Path}";
}

public interface IReference
{
    string Definition { get; }
}

public sealed record CueDefinitionReference(string Definition) : CueValueNode(Definition), IReference
{
    public override string ToString() => $"Reference to {Definition}";
}

public sealed record CueDisjunctionReference(string Definition) : CueValueNode(Definition), IReference
{
    public override string ToString() => $"Disjunction reference to {Definition}";
}

public sealed record CueStructValue(string Path, IReadOnlyList<CueStructField> Fields)
    : CueValueNode(Path)
{
    public override string ToString() => $"Struct({string.Join(", \n", Fields.Select(f => $"{f.Name}: {f.Value}"))}) at {Path}";
}

public sealed record CueStructField(string Name, CueValueNode Value, bool Optional = false);

public sealed record CueDisjunction(
    string Path,
    IReadOnlyList<CueValueNode> Branches,
    string? DiscriminatorField,
    Dictionary<string, string> BranchPaths)
    : CueValueNode(Path)
{
    public bool IsDiscriminated => DiscriminatorField != null;

    public override string ToString() =>
        $"""
         Disjunction({Branches.Count} branches:
         {string.Join("\n", Branches.Select(e => e.ToString()))}) at {Path}
         """;
}

public sealed record CueListValue : CueValueNode
{
    public CueValueNode? Tail { get; init; }
    public IReadOnlyList<CueValueNode> Indexed { get; init; }
    
    public CueListValue(
        string Path, 
        CueValueNode? Tail, 
        IReadOnlyList<CueValueNode> Indexed) : base(Path)
    {
        this.Tail = Tail;
        this.Indexed = Indexed;

        if (Tail == null && Indexed.Count == 0)
        {
            throw new InvalidDataException("either provide any index or elements or both");
        }
    }

    public override string ToString()
    {
        if (Indexed.Count == 0)
        {
            return $"[...{Tail}] at {Path}";
        }
        
        var elements = string.Join(", ", Indexed);

        return Tail is null
            ? $"[{elements}] at {Path}"
            : $"[{elements}, ...{Tail}] at {Path}";
    }
}
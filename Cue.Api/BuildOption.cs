namespace Cuelang.Cue;

public abstract record BuildOption
{
    public sealed record FileName(string Name) : BuildOption;

    public sealed record ImportPath(string Path) : BuildOption;

    public sealed record InferBuiltins(bool Value) : BuildOption;

    public sealed record Scope(Value Value) : BuildOption;
}


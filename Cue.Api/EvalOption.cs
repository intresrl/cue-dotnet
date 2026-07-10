namespace Cuelang.Cue;

public abstract record EvalOption
{
    public sealed record All : EvalOption;
    public sealed record Attributes(bool Value) : EvalOption;
    public sealed record Concrete(bool Value) : EvalOption;
    public sealed record Definitions(bool Value) : EvalOption;
    public sealed record DisallowCycles(bool Value) : EvalOption;
    public sealed record Docs(bool Value) : EvalOption;
    public sealed record ErrorsAsValues(bool Value) : EvalOption;
    public sealed record Final : EvalOption;
    public sealed record Hidden(bool Value) : EvalOption;
    public sealed record InlineImports(bool Value) : EvalOption;
    public sealed record Optionals(bool Value) : EvalOption;
    public sealed record Raw : EvalOption;
    public sealed record Schema : EvalOption;
}


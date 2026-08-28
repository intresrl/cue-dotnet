using System.Collections.Generic;

public sealed class CueList<TConcrete, TAnyIndex>
{
    public required TConcrete Concrete { get; init; }
    public List<TAnyIndex> AnyIndex { get; init; } = [];
}

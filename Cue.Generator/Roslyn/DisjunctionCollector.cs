namespace Cue.Generator.Roslyn;

public record CollectionResult(
    IReadOnlyList<CueStructValue> Structs,
    IReadOnlyList<CueDisjunction> Disjunctions,
    IReadOnlyList<CueValueNode> OtherDefinitions
);

public static class DisjunctionCollector
{
    public static CollectionResult Visit(IEnumerable<CueValueNode> nodes)
    {
        var nodeArray = nodes.ToArray();
        
        var allDefinitions = nodeArray
            .BreadthFirstSearch<CueValueNode>(n => n switch
            {
                CueDisjunction d => (d.Branches, [d]),
                CueStructValue s => (s.Fields.Select(f => f.Value), [s]),
                CueListValue l => (l.Tail is {} a ? [..l.Indexed, a] : l.Indexed, []),
                CueNullable nu => ([nu.Value], []),
                _ => ([], [])
            })
            .ToArray();

        // first pass - deduplicate definitions
        var definitionDict = new Dictionary<CueValueNode, CueValueNode>(new CueValueNodeComparer());
        foreach (var s in allDefinitions) definitionDict.TryAdd(s, s);

        var sList = new List<CueStructValue>();
        var dList = new List<CueDisjunction>();
        
        // second pass - at top level, replace all struct fields with a reference to the path
        foreach (var s in definitionDict.Keys)
        {
            switch (s)
            {
                case CueStructValue sVal:
                {
                    var structWithRef = new CueStructValue(s.Path, sVal.Fields
                        .Select(f => f with { Value = ConvertToReferences(definitionDict, f.Value) })
                        .ToList());
                    
                    sList.Add(structWithRef);
                    break;
                }
                case CueDisjunction dVal:
                    // TODO: consider what to do if a disjunction branch is itself a disjunction. Should probably resolved
                    //  as libcue discovery time

                    var disjunctionWithRef = new CueDisjunction(
                        dVal.Path,
                        dVal.Branches
                            .Select(b => ConvertToReferences(definitionDict, b))
                            .ToList(),
                        dVal.DiscriminatorField,
                        dVal.BranchPaths
                    );

                    dList.Add(disjunctionWithRef);
                    break;
            }
        }

        return new CollectionResult(
            sList, 
            dList, 
            nodeArray
                .Select(e => ConvertToReferences(definitionDict, e))
                .Where(e => e is not IReference)
                .ToList());
    }
    
    private static CueValueNode ConvertToReferences(Dictionary<CueValueNode, CueValueNode> definitionDict, CueValueNode f)
    {
        return f switch
        {
            CueDisjunction dd => new CueDisjunctionReference(definitionDict[dd].Path),
            CueStructValue ss => new CueDefinitionReference(definitionDict[ss].Path),
            CueListValue l => l with
            {
                Tail = l.Tail is { } a
                    ? ConvertToReferences(definitionDict, a)
                    : null,
                Indexed = l.Indexed
                    .Select(e => ConvertToReferences(definitionDict, e))
                    .ToArray()
            },
            CueNullable n => n with { Value = ConvertToReferences(definitionDict, n.Value) },
            _ => f
        };
    }
}
namespace Cue.Generator.Roslyn;

public record CollectionResult(
    IReadOnlyList<CueStructValue> Structs,
    IReadOnlyList<CueDisjunction> Disjunctions,
    
    // TODO: consider how to use this in the future when primitive type expression constraints will be implemented
    IReadOnlyList<CueValueNode> Other
);

public class DisjunctionCollector
{
    private readonly Dictionary<CueValueNode, CueValueNode> _definitions = new(new CueValueNodeComparer());

    public CollectionResult Visit(IEnumerable<CueValueNode> nodes)
    {
        var nodeArray = nodes.ToArray();
        
        var allDefinitions = nodeArray
            .BreadthFirstSearch<CueValueNode>(n => n switch
            {
                CueDisjunction d => (d.Branches, [d]),
                CueStructValue s => (s.Fields.Select(f => f.Value), [s]),
                CueListValue l => ([l.ElementType], []),
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
                    
                    _definitions[s] = structWithRef;
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

                    _definitions[s] = disjunctionWithRef;
                    dList.Add(disjunctionWithRef);
                    break;
            }
        }

        return new CollectionResult(sList, dList, nodeArray.Select(Visit).ToList());
    }

    private CueValueNode Visit(CueValueNode node)
    {
        return node switch
        {
            CueBottomValue or CueTopValue or CueTopValue or CueNullValue => node,

            CueStructValue s => new CueDefinitionReference(_definitions[s].Path),
            CueListValue l => new CueListValue(l.Path, Visit(l.ElementType)),
            CueDisjunction d => new CueDisjunctionReference(_definitions[d].Path),

            // be careful about these in the future. They will contain constraints potentially
            _ => node
        };
    }

    private static CueValueNode ConvertToReferences(Dictionary<CueValueNode, CueValueNode> definitionDict, CueValueNode f)
    {
        return f switch
        {
            CueDisjunction dd => new CueDisjunctionReference(definitionDict[dd].Path),
            CueStructValue ss => new CueDefinitionReference(definitionDict[ss].Path),
            CueListValue l => l with { ElementType = ConvertToReferences(definitionDict, l.ElementType) },
            CueNullable n => n with { Value = ConvertToReferences(definitionDict, n.Value) },
            _ => f
        };
    }
}
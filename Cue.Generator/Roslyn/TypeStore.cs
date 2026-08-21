namespace Cue.Generator.Roslyn;

public sealed record DisjunctionDefinition(string DisjunctionPath, string[] BranchPaths);
public sealed record ConcreteDefinition(CueStructValue StructNode);

public interface ITypeStore
{
    void Collect(IEnumerable<CueValueNode> node);
    TypeName GetTypeName(CueValueNode fieldValue);
    IEnumerable<DisjunctionDefinition> GetAbstractDefinitions();
    IEnumerable<ConcreteDefinition> GetConcreteDefinitions();
}

public class TypeStore : ITypeStore
{
    // map from disjunction path -> (base class name, discriminator field, branch paths)
    private readonly Dictionary<string, DisjunctionDefinition> _discriminatedUnions = new();

    // map from struct path -> generated type name
    private readonly Dictionary<string, CueStructValue> _concreteTypes = [];

    public void Collect(IEnumerable<CueValueNode> node)
    {
        var dd = new DisjunctionCollector();
        var (nodeArray, disjunctions) = dd.Visit(node);
        
        // collect struct nodes and assign type names
        var concreteDefs = nodeArray.BreadthFirstSearch<CueStructValue>(n => n switch
        {
            CueStructValue s => (s.Fields.Select(f => f.Value), [s]),
            CueListValue l => ([l.ElementType], []),
            CueDisjunction d => (d.Branches, []),
            _ => ([], [])
        });

        foreach (var v in concreteDefs) _concreteTypes.Add(v.Path, v);

        // collect discriminated unions and map inline branches to named structs
        foreach (var d in disjunctions)
        {
            // Remove inline discriminated union definitions from the type names
            // They should not generate their own classes
            _concreteTypes.Remove(d.Path);

            _discriminatedUnions[d.Path] = new DisjunctionDefinition(d.Path, d.Branches.Select(b => b.Path).ToArray());
        }
    }

    public IEnumerable<DisjunctionDefinition> GetAbstractDefinitions()
    {
        return _discriminatedUnions.Values.OrderBy(kv => kv.DisjunctionPath);
    }

    public TypeName GetTypeName(CueValueNode node)
    {
        return node switch
        {
            CueDefinitionReference {Definition: var def} => _discriminatedUnions.ContainsKey(def) 
                ? TypeName.FromBaseTypePath(def) 
                : TypeName.FromTypePath(def),
            CueDisjunction d => TypeName.FromBaseTypePath(d.Path),
            CueStructValue s => TypeName.FromTypePath(s.Path),
            CueListValue l => $"List<{GetTypeName(l.ElementType)}>",
            CueBoolValue => $"bool",
            CueIntValue => $"long",
            CueFloatValue => $"double",
            CueStringValue => $"string",
            CueBytesValue => $"byte[]",
            CueNumberValue => $"decimal",
            CueNullValue => $"object",
            CueBottomValue => throw new InvalidOperationException($"CueBottomValue at {node.Path} cannot be serialized"),
            _ => $"object"
        };
    }

    public IEnumerable<ConcreteDefinition> GetConcreteDefinitions()
    {
        // create classes for each struct (keep deterministic order)
        foreach (var (_, str) in _concreteTypes.OrderBy(kv => kv.Key))
        {
            yield return new ConcreteDefinition(str);
        }
    }
}
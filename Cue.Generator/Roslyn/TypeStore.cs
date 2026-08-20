namespace Cue.Generator.Roslyn;

public sealed record DisjunctionDefinition(string DisjunctionPath, string[] BranchPaths);
public sealed record ConcreteDefinition(CueStructValue StructNode);

public interface ITypeStore
{
    void Collect(CueValueNode node);
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

    public void Collect(CueValueNode node)
    {
        // collect struct nodes and assign type names
        var concreteDefs = node.BreadthFirstSearch<CueStructValue>(n => n switch
        {
            CueStructValue s => (s.Fields.Select(f => f.Value), [s]),
            CueListValue l => ([l.ElementType], []),
            CueDisjunction d => (d.Branches, []),
            _ => ([], [])
        });

        foreach (var v in concreteDefs) _concreteTypes.Add(v.Path, v);

        // collect discriminated unions and map inline branches to named structs
        var discriminations = node.BreadthFirstSearch<DisjunctionDefinition>(n => n switch
        {
            CueDisjunction d => (d.Branches, [
                new DisjunctionDefinition(d.Path, d.Branches.Select(b => b.Path).ToArray())
            ]),
            CueStructValue s => (s.Fields.Select(f => f.Value), []),
            CueListValue l => ([l.ElementType], []),
            _ => ([], [])
        });

        foreach (var d in discriminations)
        {
            // Remove inline discriminated union definitions from the type names
            // They should not generate their own classes
            _concreteTypes.Remove(d.DisjunctionPath);
            
            _discriminatedUnions[d.DisjunctionPath] = d;
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
            CueDisjunction d => 
                FindDiscriminatorBaseClass(d.Path) is {} p 
                    ? TypeName.FromBaseTypePath(p)
                    : $"object",

            CueStructValue s => TypeName.FromTypePath(s.Path),
            CueListValue l => $"List<{GetTypeName(l.ElementType)}>", // TODO: fix bug where this gets rendered as List<{ 0 }> in code
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
    
    private string? FindDiscriminatorBaseClass(string structPath)
    {
        return _discriminatedUnions.TryGetValue(structPath, out var unionInfo) 
            ? unionInfo.DisjunctionPath
            : null;
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
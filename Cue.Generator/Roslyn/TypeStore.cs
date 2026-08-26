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
        var (structs, disjunctions, _) = dd.Visit(node);
        
        // collect struct nodes and assign type names
        foreach (var v in structs)
        {
            _concreteTypes.Add(v.Path, v);
        }

        // collect discriminated unions and map inline branches to named structs
        foreach (var d in disjunctions)
        {
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
            // we need these branches only to handle the actual interface and class definitions
            CueDisjunction d => TypeName.FromDisjunctionRef(d.Path),
            CueStructValue s => TypeName.FromDefinitionRef(s.Path),
            
            // these branches are instead to fetch the type of struct fields
            CueDisjunctionReference {Definition: var def} => TypeName.FromDisjunctionRef(def),
            CueDefinitionReference {Definition: var def} => TypeName.FromDefinitionRef(def),
            CueListValue l => $"List<{GetTypeName(l.AnyIndexElement ?? new CueTopValue(l.Path))}>", // TODO: fix and implement tuple elements here once discrete indices will be implemented
            CueNullable l => $"{GetTypeName(l.Value)}?",
            CueBoolValue => $"bool",
            CueIntValue => $"long",
            CueFloatValue => $"double",
            CueStringValue => $"string",
            CueBytesValue => $"byte[]",
            CueNumberValue => $"decimal",
            CueNullValue => $"object",
            
            _ => throw new ArgumentOutOfRangeException(nameof(node), node, node.GetType() + " not supported")
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
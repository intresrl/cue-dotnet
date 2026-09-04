namespace Cue.Generator.Roslyn;

public sealed record DisjunctionDefinition(string DisjunctionPath, string[] BranchPaths);
public sealed record ConcreteDefinition(CueValueNode ValueNode);

public interface ITypeStore
{
    void Collect(IEnumerable<CueValueNode> node);
    TypeName GetTypeName(CueValueNode fieldValue);
    IEnumerable<DisjunctionDefinition> GetAbstractDefinitions();
    IEnumerable<ConcreteDefinition> GetConcreteDefinitions();
    CueExpr? GetConstraint(string typePath);
    string? GetValueType(string typePath);
}

public class TypeStore : ITypeStore
{
    // map from disjunction path -> (base class name, discriminator field, branch paths)
    private readonly Dictionary<string, DisjunctionDefinition> _discriminatedUnions = new();

    // map from type path -> value node
    private readonly Dictionary<string, CueValueNode> _concreteTypes = [];

    // map from type path -> constraint expression
    private readonly Dictionary<string, CueExpr> _constraints = [];

    // map from type path -> value type
    private readonly Dictionary<string, string> _valueTypes = [];

    public void Collect(IEnumerable<CueValueNode> nodes)
    {
        var (structs, _, other) = new DisjunctionCollector().Visit(nodes);

        foreach (var v in structs)
        {
            _concreteTypes.Add(v.Path, v);
        }

        foreach (var v in other)
        {
            var (constraint, type) = v switch
            {
                CueIntValue { Constraint: { } c } => (c, NumberBoundExtensions.GetBoundsType(c)),
                CueIntValue or CueNumberValue => (null, "BigInteger"),
                CueFloatValue { Constraint: var c } => (c, "double"),
                CueStringValue { Constraint: var c } => (c, "string"),
                CueBoolValue { Constraint: var c } => (c, "bool"),
                _ => default,
            };

            if (type == null)
            {
                continue;
            }

            _concreteTypes.Add(v.Path, v);
            _valueTypes[v.Path] = type;
            if (constraint is not null)
            {
                _constraints[v.Path] = constraint;
            }
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

    public CueExpr? GetConstraint(string typePath)
    {
        return _constraints.GetValueOrDefault(typePath);
    }

    public string? GetValueType(string typePath)
    {
        return _valueTypes.GetValueOrDefault(typePath);
    }

    public IEnumerable<ConcreteDefinition> GetConcreteDefinitions()
    {
        // create classes for each struct (keep deterministic order)
        foreach (var (_, node) in _concreteTypes.OrderBy(kv => kv.Key))
        {
            yield return new ConcreteDefinition(node);
        }
    }
}
namespace Cue.Generator.Roslyn;

public sealed record DisjunctionDefinition(string DisjunctionPath, string[] BranchPaths);
public sealed record ConcreteDefinition(CueValueNode ValueNode);
public sealed record ContainerDefinition(string ListPath, TypeName ListType);

public interface ITypeStore
{
    void Collect(IEnumerable<CueValueNode> nodes);
    TypeName GetTypeName(CueValueNode fieldValue);
    IEnumerable<DisjunctionDefinition> GetAbstractDefinitions();
    IEnumerable<ConcreteDefinition> GetConcreteDefinitions();
    IEnumerable<ContainerDefinition> GetContainerDefinitions();
    CueExpr? GetConstraint(string typePath);
    string? GetValueType(string typePath);
}

public class TypeStore : ITypeStore
{
    private CollectionResult _collectionResult = new([], [], []);
    private readonly Dictionary<string, CueValueNode> _concreteTypes = [];
    private readonly Dictionary<string, CueExpr> _constraints = [];
    private readonly Dictionary<string, string> _valueTypes = [];

    public void Collect(IEnumerable<CueValueNode> nodes)
    {
        _collectionResult = DisjunctionCollector.Visit(nodes);

        foreach (var value in _collectionResult.Structs)
        {
            _concreteTypes[value.Path] = value;
        }

        foreach (var value in _collectionResult.OtherDefinitions)
        {
            var (constraint, type) = value switch
            {
                CueIntValue { Constraint: { } c } => (c, NumberBoundExtensions.GetBoundsType(c)),
                CueIntValue or CueNumberValue => (null, "BigInteger"),
                CueFloatValue { Constraint: var c } => (c, "double"),
                CueStringValue { Constraint: var c } => (c, "string"),
                CueBoolValue { Constraint: var c } => (c, "bool"),
                _ => (null, null)
            };

            if (type is null)
            {
                continue;
            }

            _concreteTypes[value.Path] = value;
            _valueTypes[value.Path] = type;
            if (constraint is not null)
            {
                _constraints[value.Path] = constraint;
            }
        }
    }

    public IEnumerable<DisjunctionDefinition> GetAbstractDefinitions()
    {
        return _collectionResult.Disjunctions
            .OrderBy(d => d.Path)
            .Select(d => new DisjunctionDefinition(
                d.Path,
                d.Branches.Select(b => b.Path).ToArray()));
    }

    public IEnumerable<ContainerDefinition> GetContainerDefinitions()
    {
        return _collectionResult.OtherDefinitions
            .OfType<CueListValue>()
            .OrderBy(l => l.Path)
            .Select(l => new ContainerDefinition(l.Path, GetTypeName(l)));
    }

    public TypeName GetTypeName(CueValueNode node)
    {
        return node switch
        {
            CueDisjunction d => TypeName.FromRef(d.Path, NamingKind.Disjunction),
            CueStructValue s => TypeName.FromRef(s.Path, NamingKind.Type),
            CueDisjunctionReference { Definition: var definition } =>
                TypeName.FromRef(definition, NamingKind.Disjunction),
            CueDefinitionReference { Definition: var definition } =>
                TypeName.FromRef(definition, NamingKind.Type),
            CueListValue list => GetListTypeName(list),
            CueNullable nullable => $"{GetTypeName(nullable.Value)}?",
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

    private TypeName GetListTypeName(CueListValue list)
    {
        return list switch
        {
            { Indexed: [], Tail: { } tail } => $"List<{GetTypeName(tail)}>",
            { Indexed: { Count: > 0 } indexed, Tail: null } => GetTupleTypeName(indexed),
            { Indexed: { Count: > 0 } indexed, Tail: { } tail } =>
                $"CueList<{GetTupleTypeName(indexed)}, {GetTypeName(tail)}>",
            _ => throw new ArgumentOutOfRangeException(nameof(list), list, null)
        };
    }

    private TypeName GetTupleTypeName(IReadOnlyList<CueValueNode> elements)
    {
        return elements is [{ } single]
            ? (TypeName)$"ValueTuple<{GetTypeName(single)}>"
            : (TypeName)$"({TypeName.Join($", ", elements.Select(GetTypeName))})";
    }

    public CueExpr? GetConstraint(string typePath) => _constraints.GetValueOrDefault(typePath);

    public string? GetValueType(string typePath) => _valueTypes.GetValueOrDefault(typePath);

    public IEnumerable<ConcreteDefinition> GetConcreteDefinitions()
    {
        return _concreteTypes
            .OrderBy(kv => kv.Key)
            .Select(kv => new ConcreteDefinition(kv.Value));
    }
}

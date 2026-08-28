namespace Cue.Generator.Roslyn;

public sealed record DisjunctionDefinition(string DisjunctionPath, string[] BranchPaths);
public sealed record ContainerDefinition(string ListPath, TypeName ListType);
public sealed record RecordDefinition(CueStructValue StructNode);

public interface ITypeStore
{
    void Collect(IEnumerable<CueValueNode> node);
    TypeName GetTypeName(CueValueNode fieldValue);
    IEnumerable<DisjunctionDefinition> GetAbstractDefinitions();
    IEnumerable<RecordDefinition> GetRecordDefinitions();
    IEnumerable<ContainerDefinition> GetContainerDefinitions();
}

public class TypeStore : ITypeStore
{
    private CollectionResult _collectionResult;

    public void Collect(IEnumerable<CueValueNode> node)
    {
        _collectionResult = DisjunctionCollector.Visit(node);
    }

    public IEnumerable<DisjunctionDefinition> GetAbstractDefinitions()
    {
        return _collectionResult.Disjunctions
            .OrderBy(kv => kv.Path)
            .Select(d => new DisjunctionDefinition(
                d.Path, 
                d.Branches.Select(b => b.Path).ToArray()
            )
        );
    }
    
    public IEnumerable<ContainerDefinition> GetContainerDefinitions()
    {
        return _collectionResult.OtherDefinitions
            .OrderBy(kv => kv.Path)
            .Select(d => new ContainerDefinition(d.Path, GetTypeName(d)));
    }

    public TypeName GetTypeName(CueValueNode node)
    {
        return node switch
        {
            // we need these branches only to handle the actual interface and class definitions
            CueDisjunction d => TypeName.FromRef(d.Path, NamingKind.Disjunction),
            CueStructValue s => TypeName.FromRef(s.Path, NamingKind.Type),
            
            // these branches are instead to fetch the type of struct fields
            CueDisjunctionReference {Definition: var def} => TypeName.FromRef(def, NamingKind.Disjunction),
            CueDefinitionReference {Definition: var def} => TypeName.FromRef(def, NamingKind.Type),
            CueListValue l => GetListTypeName(l),
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

    private TypeName GetListTypeName(CueListValue list)
    {
        return list switch
        {
            { Indexed: [], Tail: { } tail } => $"List<{GetTypeName(tail)}>",
            { Indexed: { Count : > 0 } indexed, Tail: null } => GetTupleTypeName(indexed),
            { Indexed: { Count : > 0 } indexed, Tail: { } tail } =>
                $"CueList<{GetTupleTypeName(indexed)}, {GetTypeName(tail)}>",
            
            _ => throw new ArgumentOutOfRangeException(nameof(list), list, null)
        };
    }

    private TypeName GetTupleTypeName(IReadOnlyList<CueValueNode> elements)
    {
        return elements is [{ } single] 
            ? (TypeName)$"ValueTuple<{GetTypeName(single)}>" 
            : $"({TypeName.Join($", ", elements.Select(GetTypeName))})";
    }

    public IEnumerable<RecordDefinition> GetRecordDefinitions()
    {
        return _collectionResult.Structs
            .OrderBy(kv => kv.Path)
            .Select(e => new RecordDefinition(e));
    }
}
namespace Cue.Generator.Roslyn;

public sealed record AbstractDefinition(string TypeName);

public sealed record ConcreteDefinition(string TypeName, CueStructValue StructNode, string? BaseClassName);

public interface ITypeStore
{
    void Collect(CueValueNode node);
    void CollectionReport();
    string GetTypeName(CueValueNode fieldValue);
    IEnumerable<AbstractDefinition> GetAbstractDefinitions();
    IEnumerable<ConcreteDefinition> GetConcreteDefinitions();
}

internal sealed record DisjunctionDefinition(
    CueDisjunction Disjunction,
    string BaseClassName,
    List<string> BranchPaths);

public class TypeStore(
    IIdentifierNamer namer,
    IEqualityComparer<CueStructValue> comparer,
    TextWriter? debugWriter = null) : ITypeStore
{
    // Temporary storage during code generation
    private CueValueNode? _currentRoot;

    // map from disjunction path -> (base class name, discriminator field, branch paths)
    private readonly Dictionary<string, DisjunctionDefinition> _discriminatedUnions = new();

    // map from struct path -> generated type name
    private readonly Dictionary<string, string> _concreteTypeNames = new();

    public void Collect(CueValueNode node)
    {
        _currentRoot = node;

        // collect struct nodes and assign type names
        var concreteDefs = node.BreadthFirstSearch<CueStructValue>(n => n switch
        {
            CueStructValue s => (s.Fields.Select(f => f.Value), [s]),
            CueListValue l => ([l.ElementType], []),
            CueDisjunction d => (d.Branches, []),
            _ => ([], [])
        });

        foreach (var v in concreteDefs) _concreteTypeNames.TryAdd(v.Path, namer.TypeName(v.Path));

        // collect discriminated unions and map inline branches to named structs
        var discriminations = node.BreadthFirstSearch<DisjunctionDefinition>(n => n switch
        {
            CueDisjunction { IsDiscriminated: true } d => (d.Branches, [
                new DisjunctionDefinition(
                    d,
                    namer.BaseClassName(d.Path),
                    MapBranchesToNamedStructs(d)
                )
            ]),
            CueStructValue s => (s.Fields.Select(f => f.Value), []),
            CueListValue l => ([l.ElementType], []),
            _ => ([], [])
        });

        foreach (var d in discriminations)
        {
            // Remove inline discriminated union definitions from the type names
            // They should not generate their own classes
            _concreteTypeNames.Remove(d.Disjunction.Path);
            
            _discriminatedUnions[d.Disjunction.Path] = d;
        }
    }

    public IEnumerable<AbstractDefinition> GetAbstractDefinitions()
    {
        foreach (var (_, (_, baseClassName, _)) in _discriminatedUnions.OrderBy(kv => kv.Value.BaseClassName))
            yield return new AbstractDefinition(baseClassName);
    }

    public void CollectionReport()
    {
        if (debugWriter == null) return;

        debugWriter.WriteLine("=== Collected Structs ===");
        foreach (var (path, typeName) in _concreteTypeNames) debugWriter.WriteLine($"{path} -> {typeName}");

        debugWriter.WriteLine();

        debugWriter.WriteLine("=== Collected Discriminated Unions ===");
        foreach (var (path, (baseClass, discriminator, branches)) in _discriminatedUnions)
        {
            debugWriter.WriteLine($"{path} -> BaseClass: {baseClass}, Discriminator: {discriminator}");
            foreach (var branch in branches) debugWriter.WriteLine($"  - {branch}");
        }

        debugWriter.WriteLine();
        debugWriter.WriteLine("=== Disjunction Details ===");
        foreach (var disjunction in FindAllDisjunctions(_currentRoot))
        {
            debugWriter.WriteLine($"Disjunction at {disjunction.Path}:");
            debugWriter.WriteLine($"  IsDiscriminated: {disjunction.IsDiscriminated}");
            debugWriter.WriteLine($"  DiscriminatorField: {disjunction.DiscriminatorField}");
            debugWriter.WriteLine($"  BranchPaths ({disjunction.BranchPaths.Count}):");
            foreach (var (key, value) in disjunction.BranchPaths) debugWriter.WriteLine($"    {key} -> {value}");

            debugWriter.WriteLine($"  Branches ({disjunction.Branches.Count}):");
            foreach (var branch in disjunction.Branches)
            {
                debugWriter.WriteLine($"    {branch.GetType().Name} at {branch.Path}");
                if (branch is not CueStructValue sv) continue;

                foreach (var field in sv.Fields)
                    debugWriter.WriteLine($"      - {field.Name}: {field.Value.GetType().Name}");
            }
        }

        debugWriter.Flush();
    }

    private static CueStructValue? FindStruct(CueValueNode root, string path)
    {
        var results = root.BreadthFirstSearch<CueStructValue>(n => n switch
        {
            CueStructValue s when s.Path == path => ([], [s]),
            CueStructValue sv => (sv.Fields.Select(f => f.Value), []),
            CueListValue l => ([l.ElementType], []),
            CueDisjunction d => (d.Branches, []),
            _ => ([], [])
        });

        return results.FirstOrDefault();
    }


    private string? FindMatchingNamedStruct(CueStructValue inlineStruct)
    {
        if (_currentRoot == null) return null;

        // Find a named struct that matches this inline struct by comparing fields
        foreach (var (namedPath, _) in _concreteTypeNames)
        {
            if (FindStruct(_currentRoot, namedPath) is not { } namedStruct) continue;

            if (comparer.Equals(inlineStruct, namedStruct)) return namedPath;
        }

        return null;
    }

    private string ListTypeName(CueListValue list)
    {
        var elemType = GetTypeName(list.ElementType);
        return $"List<{elemType}>";
    }

    public string GetTypeName(CueValueNode node)
    {
        return node switch
        {
            CueStructValue s => _concreteTypeNames.GetValueOrDefault(s.Path, "object"),
            CueDisjunction { IsDiscriminated: true } d => FindDiscriminatorBaseClass(d.Path) ?? "object",
            CueListValue l => ListTypeName(l),
            CueBoolValue => "bool",
            CueIntValue => "long",
            CueFloatValue => "double",
            CueStringValue => "string",
            CueBytesValue => "byte[]",
            CueNumberValue => "decimal", // todo: should support long + double
            CueNullValue => "object", // TODO: Fix serialization of CueNullValue - currently serialized as object type
            CueBottomValue =>
                throw new InvalidOperationException($"CueBottomValue at {node.Path} cannot be serialized"),
            _ => "object"
        };
    }

    public string? FindDiscriminatorBaseClass(string structPath)
    {
        // Check if this path is a discriminated union itself
        if (_discriminatedUnions.TryGetValue(structPath, out var unionInfo)) return unionInfo.BaseClassName;

        // Also check if this path is a branch of any discriminated union (for backward compatibility)
        foreach (var (_, (_, name, branchPaths)) in _discriminatedUnions)
            if (branchPaths.Contains(structPath))
                return name;

        return null;
    }

    public IEnumerable<ConcreteDefinition> GetConcreteDefinitions()
    {
        // create classes for each struct (keep deterministic order)
        foreach (var (path, typeName) in _concreteTypeNames.OrderBy(kv => kv.Value))
        {
            if (FindStruct(_currentRoot, path) is not { } structNode) continue;

            // Check if this struct is part of a discriminated union
            var baseClass = FindDiscriminatorBaseClass(path);
            yield return new ConcreteDefinition(typeName, structNode!, baseClass);
        }
    }

    private List<string> MapBranchesToNamedStructs(CueDisjunction disjunction)
    {
        var branchPaths = new List<string>();

        if (debugWriter != null)
        {
            debugWriter.WriteLine($"  MapBranchesToNamedStructs for {disjunction.Path}:");
            debugWriter.WriteLine($"    DiscriminatorField: {disjunction.DiscriminatorField}");
            debugWriter.WriteLine($"    Branches count: {disjunction.Branches.Count}");
        }

        // Get all inline struct branches
        var inlineStructBranches = disjunction.Branches.OfType<CueStructValue>().ToList();

        debugWriter?.WriteLine($"    Inline struct branches: {inlineStructBranches.Count}");

        // Match each inline struct to a named struct
        foreach (var inlineStruct in inlineStructBranches)
        {
            var matchedPath = FindMatchingNamedStruct(inlineStruct);
            if (matchedPath != null)
            {
                if (debugWriter != null)
                {
                    var discriminatorField = inlineStruct.Fields
                        .FirstOrDefault(f => f.Name == disjunction.DiscriminatorField);
                    var discriminatorValue = (discriminatorField?.Value as CueStringValue)?.ConcreteValue ?? "unknown";
                    debugWriter.WriteLine(
                        $"  Mapped inline {inlineStruct.Path} ({discriminatorValue}) to {matchedPath}");
                }

                branchPaths.Add(matchedPath);
            }
            else
            {
                debugWriter?.WriteLine($"  Could not map inline {inlineStruct.Path} - using as-is");

                branchPaths.Add(inlineStruct.Path);
            }
        }

        debugWriter?.WriteLine($"    Total mapped branches: {branchPaths.Count}");

        return branchPaths;
    }

    private static IEnumerable<CueDisjunction> FindAllDisjunctions(CueValueNode node)
    {
        return node.BreadthFirstSearch<CueDisjunction>(n => n switch
        {
            CueDisjunction d => (d.Branches, [d]),
            CueStructValue s => (s.Fields.Select(f => f.Value), []),
            CueListValue l => ([l.ElementType], []),
            _ => ([], [])
        });
    }
}
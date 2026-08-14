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

public class TypeStore(
    IIdentifierNamer namer,
    IEqualityComparer<CueStructValue> comparer,
    TextWriter? debugWriter = null) : ITypeStore
{
    // Temporary storage during code generation
    private CueValueNode? _currentRoot;

    // map from disjunction path -> (base class name, discriminator field, branch paths)
    private readonly Dictionary<string, (string BaseClassName, string DiscriminatorField, List<string> BranchPaths)>
        _discriminatedUnions = new();

    // map from struct path -> generated type name
    private readonly Dictionary<string, string> _typeNames = new();

    public void Collect(CueValueNode node)
    {
        _currentRoot = node;

        // collect struct nodes and assign type names
        CollectStructs(node, new HashSet<string>(StringComparer.Ordinal));

        // collect discriminated unions and map inline branches to named structs
        CollectDiscriminatedUnions(node, new HashSet<string>(StringComparer.Ordinal));

        // Remove inline discriminated union definitions from the type names
        // They should not generate their own classes
        var inlineUnionPaths = _discriminatedUnions.Keys.ToList();
        foreach (var unionPath in inlineUnionPaths) _typeNames.Remove(unionPath);
    }

    private void CollectStructs(CueValueNode node, HashSet<string> visitedStructPaths)
    {
        switch (node)
        {
            case CueStructValue s:
            {
                // Avoid revisiting the same struct path to prevent infinite recursion on self-references
                if (!visitedStructPaths.Add(s.Path)) return;

                var typeName = namer.TypeName(s.Path);
                _typeNames.TryAdd(s.Path, typeName);

                foreach (var f in s.Fields) CollectStructs(f.Value, visitedStructPaths);

                break;
            }
            case CueListValue l:
            {
                CollectStructs(l.ElementType, visitedStructPaths);
                break;
            }
            case CueDisjunction d:
            {
                foreach (var branch in d.Branches) CollectStructs(branch, visitedStructPaths);

                break;
            }
        }
    }

    public IEnumerable<AbstractDefinition> GetAbstractDefinitions()
    {
        foreach (var (_, (baseClassName, _, _)) in _discriminatedUnions.OrderBy(kv => kv.Value.BaseClassName))
            yield return new AbstractDefinition(baseClassName);
    }

    public void CollectionReport()
    {
        if (debugWriter == null) return;

        debugWriter.WriteLine("=== Collected Structs ===");
        foreach (var (path, typeName) in _typeNames) debugWriter.WriteLine($"{path} -> {typeName}");

        debugWriter.WriteLine();

        debugWriter.WriteLine("=== Collected Discriminated Unions ===");
        foreach (var (path, (baseClass, discriminator, branches)) in _discriminatedUnions)
        {
            debugWriter.WriteLine($"{path} -> BaseClass: {baseClass}, Discriminator: {discriminator}");
            foreach (var branch in branches) debugWriter.WriteLine($"  - {branch}");
        }

        debugWriter.WriteLine();
        debugWriter.WriteLine("=== Disjunction Details ===");
        var result = new List<(string, CueDisjunction)>();
        FindAllDisjunctions(_currentRoot, result);
        List<(string Path, CueDisjunction Disjunction)> disjunctions = result;
        foreach (var (path, disjunction) in disjunctions)
        {
            debugWriter.WriteLine($"Disjunction at {path}:");
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

    private static bool TryFindStruct(CueValueNode root, string path, out CueStructValue? result)
    {
        result = null;
        switch (root)
        {
            case CueStructValue s when s.Path == path:
                result = s;
                return true;
            case CueStructValue sv:
            {
                foreach (var f in sv.Fields)
                    if (TryFindStruct(f.Value, path, out result))
                        return true;

                break;
            }
            case CueListValue lv:
            {
                if (TryFindStruct(lv.ElementType, path, out result)) return true;

                break;
            }
            case CueDisjunction du:
            {
                foreach (var branch in du.Branches)
                    if (TryFindStruct(branch, path, out result))
                        return true;

                break;
            }
        }

        return false;
    }

    private void CollectDiscriminatedUnions(CueValueNode node, HashSet<string> visited)
    {
        while (true)
        {
            switch (node)
            {
                case CueDisjunction { IsDiscriminated: true } disjunction when !visited.Add(disjunction.Path):
                    return;
                // Generate base class name from path
                case CueDisjunction { IsDiscriminated: true } disjunction:
                {
                    var baseClassName = namer.BaseClassName(disjunction.Path);

                    // Map inline branches to named structs
                    var branchPaths = MapBranchesToNamedStructs(disjunction);

                    _discriminatedUnions[disjunction.Path] =
                        (baseClassName, disjunction.DiscriminatorField!, branchPaths);

                    foreach (var branch in disjunction.Branches)
                        CollectDiscriminatedUnions(branch, visited);

                    break;
                }
                case CueStructValue structValue when !visited.Add(structValue.Path):
                    return;
                case CueStructValue structValue:
                {
                    foreach (var field in structValue.Fields)
                        CollectDiscriminatedUnions(field.Value, visited);

                    break;
                }
                case CueListValue listValue:
                    node = listValue.ElementType;
                    continue;
                // Regular non-discriminated union - still need to visit branches
                case CueDisjunction regularDisjunction when !visited.Add(regularDisjunction.Path):
                    return;
                case CueDisjunction regularDisjunction:
                {
                    foreach (var branch in regularDisjunction.Branches)
                        CollectDiscriminatedUnions(branch, visited);

                    break;
                }
            }

            break;
        }
    }


    private string? FindMatchingNamedStruct(CueStructValue inlineStruct)
    {
        if (_currentRoot == null) return null;

        // Find a named struct that matches this inline struct by comparing fields
        foreach (var (namedPath, _) in _typeNames)
        {
            if (!TryFindStruct(_currentRoot, namedPath, out var namedStruct) || namedStruct == null) continue;

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
            CueStructValue s => _typeNames.GetValueOrDefault(s.Path, "object"),
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
        foreach (var (_, (baseClassName2, _, branchPaths)) in _discriminatedUnions)
            if (branchPaths.Contains(structPath))
                return baseClassName2;

        return null;
    }

    public IEnumerable<ConcreteDefinition> GetConcreteDefinitions()
    {
        // create classes for each struct (keep deterministic order)
        foreach (var (path, typeName) in _typeNames.OrderBy(kv => kv.Value))
        {
            if (!TryFindStruct(_currentRoot, path, out var structNode)) continue;

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

    private static void FindAllDisjunctions(CueValueNode node, List<(string Path, CueDisjunction Disjunction)> result)
    {
        while (true)
        {
            switch (node)
            {
                case CueDisjunction d:
                    result.Add((d.Path, d));
                    foreach (var branch in d.Branches) FindAllDisjunctions(branch, result);

                    break;
                case CueStructValue s:
                    foreach (var f in s.Fields) FindAllDisjunctions(f.Value, result);

                    break;
                case CueListValue l:
                    node = l.ElementType;
                    continue;
            }

            break;
        }
    }
}
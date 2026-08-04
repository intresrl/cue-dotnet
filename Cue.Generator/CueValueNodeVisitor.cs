using Cuelang.Cue;

namespace Cue.Generator;

public sealed class CueValueNodeVisitor : CueValueVisitor<CueValueNode>
{
    protected override CueValueNode VisitBottom(Value value)
    {
        return new CueBottomValue(value.Path());
    }

    protected override CueValueNode VisitNull(Value value)
    {
        return new CueNullValue(value.Path());
    }

    private static T? GetConcrete<T>(Value value, Func<Value, T> getConcrete) where T : struct
    {
        if (!value.IsConcrete()) return null;

        try
        {
            return getConcrete(value);
        }
        catch
        {
            // Ignore errors if we can't extract the concrete value
            return null;
        }
    }

    private static T? GetConcreteReference<T>(Value value, Func<Value, T> getConcrete) where T : class
    {
        if (!value.IsConcrete()) return null;

        try
        {
            return getConcrete(value);
        }
        catch
        {
            // Ignore errors if we can't extract the concrete value
            return null;
        }
    }

    protected override CueValueNode VisitBool(Value value)
    {
        return new CueBoolValue(value.Path(), GetConcrete(value, v => v.GetBoolean()));
    }

    protected override CueValueNode VisitInt(Value value)
    {
        return new CueIntValue(value.Path(), GetConcrete(value, v => v.GetLong()));
    }

    protected override CueValueNode VisitFloat(Value value)
    {
        return new CueFloatValue(value.Path(), GetConcrete(value, v => v.GetDouble()));
    }

    protected override CueValueNode VisitString(Value value)
    {
        return new CueStringValue(value.Path(), GetConcreteReference(value, v => v.GetString()));
    }

    protected override CueValueNode VisitBytes(Value value)
    {
        return new CueBytesValue(value.Path(), GetConcreteReference(value, v => v.GetBytes()));
    }

    protected override CueValueNode VisitNumber(Value value)
    {
        return new CueNumberValue(value.Path(), GetConcrete(value, v => v.GetDouble()));
    }

    protected override CueValueNode VisitTop(Value value)
    {
        // Check for disjunctions at the top level
        if (value.Disjunctions() is { Length: > 0 } disjunctions) return VisitDisjunction(value.Path(), disjunctions);

        if (value.IncompleteKind() != Kind.Top)
            // Delegate to base class incomplete kind dispatch
            return Dispatch(value, value.IncompleteKind());

        return new CueTopValue(value.Path());
    }

    protected override CueValueNode VisitStruct(Value value)
    {
        var path = value.Path();
        
        if (value.Disjunctions() is { Length: > 0 } disjunctions)
        {
            return VisitDisjunction(path, disjunctions);
        }
        
        var fieldValues = value.Fields(true);
        var fields = new List<CueStructField>(fieldValues.Length);

        foreach (var fieldValue in fieldValues)
        {
            using (fieldValue)
            {
                var childPath = fieldValue.Path();
                fields.Add(new CueStructField(GetFieldName(path, childPath), Visit(fieldValue)));
            }
        }

        return new CueStructValue(path, fields);
    }

    protected override CueValueNode VisitList(Value value)
    {
        var path = value.Path();
        using var elementValue = value.LookupAnyIndex();
        var elementType = Visit(elementValue);
        return new CueListValue(path, elementType);
    }

    private CueDisjunction VisitDisjunction(string path, Value[] disjunctions)
    {
        var branches = disjunctions.Select(Visit).ToList();
        var (name, paths) = FindDiscriminatorField(branches);
        return new CueDisjunction(path, branches, name, paths);
    }

    private (string? name, Dictionary<string, string> branches) FindDiscriminatorField(List<CueValueNode> branches)
    {
        if (branches.Count == 0)
            return (null, []);

        // All branches must be structs to have a discriminator
        if (branches.Any(b => b is not CueStructValue))
            return (null, []);

        var structBranches = branches.Cast<CueStructValue>().ToList();

        // for each branch, extract strings that have a constant value
        var namesPerBranch = structBranches
            .Select(e => e.Fields
                .Where(f => f.Value is CueStringValue { ConcreteValue: not null })
                .Select(f => new { f.Name, Value = (CueStringValue)f.Value })
            )
            .ToArray();

        var fields = namesPerBranch
            .Aggregate((a, b) => a.IntersectBy(b.Select(e => e.Name), e => e.Name))
            .ToArray();

        var name = fields.Select(field => new
            {
                field,
                allValues = structBranches
                    .Select(b => ((CueStringValue)b.Fields.First(f => f.Name == field.Name).Value).ConcreteValue)
                    .ToArray()
            })
            .Where(t => t.allValues.Distinct().Count() == t.allValues.Length)
            .Select(t => t.field.Name)
            .FirstOrDefault();

        if (name == null) return (null, []);

        var branchDict = structBranches.ToDictionary(
            b => ((CueStringValue)b.Fields.First(f => f.Name == name).Value).ConcreteValue!,
            b => b.Path
        );

        return (name, branchDict);
    }

    private static string GetFieldName(string parentPath, string childPath)
    {
        if (string.IsNullOrEmpty(parentPath))
        {
            return childPath;
        }

        var prefix = parentPath + ".";
        return childPath.StartsWith(prefix, StringComparison.Ordinal)
            ? childPath[prefix.Length..]
            : childPath;
    }
}

public static class CueValueExtensions
{
    public static CueValueNode ToCueValueNode(this Value value)
    {
        return new CueValueNodeVisitor().Visit(value);
    }
}
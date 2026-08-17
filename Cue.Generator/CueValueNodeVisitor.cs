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
        return !value.IsConcrete() ? null : getConcrete(value);
    }

    private static T? GetConcreteReference<T>(Value value, Func<Value, T> getConcrete) where T : class
    {
        return !value.IsConcrete() ? null : getConcrete(value);
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
        return new CueStringValue(value.Path(), GetConcreteReference(value, v => v.GetString()!));
    }

    protected override CueValueNode VisitBytes(Value value)
    {
        return new CueBytesValue(value.Path(), GetConcreteReference(value, v => v.GetBytes()));
    }

    protected override CueValueNode VisitNumber(Value value)
    {
        return new CueNumberValue(value.Path());
    }

    protected override CueValueNode VisitTop(Value value)
    {
        return new CueTopValue(value.Path());
    }

    protected override CueValueNode VisitStruct(Value value)
    {
        var path = value.Path();

        var fieldValues = value.Fields(new EvalOption.Optionals(true));
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

    protected override CueValueNode VisitDisjunction(Value value, IEnumerable<Value> branches)
    {
        var branchArray = branches.ToArray();
        
        try
        {
            var nodes = branchArray.Select(Visit).ToList();
            var (name, paths) = FindDiscriminatorField(nodes);
            return new CueDisjunction(value.Path(), nodes, name, paths);
        }
        finally
        {
            foreach (var branch in branchArray)
            {
                branch.Dispose();
            }
        }
    }

    private static string GetDiscriminatorValue(CueStructValue branch, string name)
    {
        var field = branch.Fields.FirstOrDefault(f => f.Name == name);
        return (field?.Value as CueStringValue)?.ConcreteValue
               ?? throw new InvalidOperationException($"branch {branch} has no discriminator property '{name}'");
    }

    private static (string? name, Dictionary<string, string> branches) FindDiscriminatorField(
        List<CueValueNode> branches)
    {
        if (branches.Count == 0)
        {
            return (null, []);
        }

        // All branches must be structs to have a discriminator
        if (branches.Any(b => b is not CueStructValue))
        {
            return (null, []);
        }

        var structBranches = branches.Cast<CueStructValue>().ToList();

        // for each branch, extract strings that have a constant value
        var namesPerBranch = structBranches
            .Select(e => e.Fields
                .Where(f => f.Value is CueStringValue { ConcreteValue: not null })
                .Select(f => new { f.Name, Value = (CueStringValue)f.Value })
            )
            .ToArray();

        // extract all discriminator candidates
        var fields = namesPerBranch
            .Aggregate((a, b) => a.IntersectBy(b.Select(e => e.Name), e => e.Name))
            .ToArray();

        var name = fields.Select(field => new
        {
            field,
            allValues = structBranches
                    .Select(b => GetDiscriminatorValue(b, field.Name))
                    .ToArray()
        })
            .Where(t => t.allValues.Distinct().Count() == t.allValues.Length)
            .Select(t => t.field.Name)
            .FirstOrDefault();

        if (name == null)
        {
            return (null, []);
        }

        var branchDict = structBranches.ToDictionary(
            b => GetDiscriminatorValue(b, name),
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

using Cuelang.Cue;

namespace Cue.Generator;

public sealed class CueValueNodeVisitor : CueValueVisitor<CueValueNode>
{
    protected override CueValueNode VisitTop(Value value)
    {
        // Check for disjunctions at the top level
        if (value.Disjunctions() is { Length: > 0 } disjunctions)
        {
            return VisitDisjunction(value.Path(), disjunctions);
        }

        if (value.IncompleteKind() != Kind.Top)
        {
            // Delegate to base class incomplete kind dispatch
            return Dispatch(value, value.IncompleteKind());
        }

        return new CueTop(value.Path());
    }

    protected override CueValueNode VisitStruct(Value value)
    {
        var path = value.Path();
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

    protected override CueValueNode VisitSimple(Value value, Kind kind)
    {
        return new CueSimpleValue(kind, value.Path());
    }

    private CueDisjunction VisitDisjunction(string path, Value[] disjunctions)
    {
        var branches = disjunctions.Select(Visit).ToList();
        
        // Create disjunction with optional discriminator
        return new CueDisjunction(path, branches, true);
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

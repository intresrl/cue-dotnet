using Cuelang.Cue;

namespace Cue.Generator;

public sealed class CueValueNodeVisitor : CueValueVisitor<CueValueNode>
{
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

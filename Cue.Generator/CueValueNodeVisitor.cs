using Cuelang.Cue;

namespace Cue.Generator;

public sealed class CueValueVisitor(Value[] rootDefinitions)
{
    private readonly HashSet<string> _definedPaths = [];
    
    public static IEnumerable<CueValueNode> VisitRoot(Value value)
    {
        var definitions = value.Fields(new EvalOption.Definitions(true));

        try
        {
            var visitor = new CueValueVisitor(definitions);
            return definitions.Select(visitor.Visit).ToArray();
        }
        finally
        {
            foreach (var definition in definitions)
            {
                definition.Dispose();
            }
            value.Dispose();
        }
    }
    
    [Obsolete]
    public static CueValueNode ForTests(Value value)
    {
        return new CueValueVisitor([]).Visit(value);
    }
    
    private static T? GetConcrete<T>(Value value, Func<Value, T> getConcrete) where T : struct
    {
        return !value.IsConcrete() ? null : getConcrete(value);
    }

    private static T? GetConcreteReference<T>(Value value, Func<Value, T> getConcrete) where T : class
    {
        return !value.IsConcrete() ? null : getConcrete(value);
    }

    private CueValueNode Visit(Value value)
    {
        foreach (var rootValue in rootDefinitions)
        {
            if (value.Equals(rootValue) && _definedPaths.Contains(rootValue.Path()))
            {
                return new CueDefinitionReference(rootValue.Path());
            }
        }
        
        _definedPaths.Add(value.Path());

        var kind = value.IncompleteKind();

        if (kind is Kind.Top or Kind.Struct &&
            DisjunctionBranches(value) is { } branches)
        {
            return VisitDisjunction(value, branches);
        }

        return kind switch
        {
            Kind.Bottom => new CueBottomValue(value.Path()),
            Kind.Null => new CueNullValue(value.Path()),
            Kind.Number => new CueNumberValue(value.Path()),
            Kind.Top => new CueTopValue(value.Path()),

            Kind.Bool => new CueBoolValue(value.Path(), GetConcrete(value, v => v.GetBoolean())),
            Kind.Int => new CueIntValue(value.Path(), GetConcrete(value, v => v.GetLong())),
            Kind.Float => new CueFloatValue(value.Path(), GetConcrete(value, v => v.GetDouble())),
            Kind.String => new CueStringValue(value.Path(), GetConcreteReference(value, v => v.GetString()!)),
            Kind.Bytes => new CueBytesValue(value.Path(), GetConcreteReference(value, v => v.GetBytes())),

            Kind.Struct => VisitStruct(value),
            Kind.List => VisitList(value),

            _ => throw new ArgumentOutOfRangeException(nameof(kind), kind, "Unexpected kind")
        };
    }

    private static IEnumerable<Value>? DisjunctionBranches(Value value)
    {
        var expr = value.Expr();

        if (expr.Op == ExprOp.Or)
        {
            return expr.Values;
        }

        // expr is `matchN(1, [...])`, where list is concrete length
        if (expr is
            {
                Op: ExprOp.Call,
                CallName: "matchN",
                Values: [{ } n, { } l]
            }
            && n.IncompleteKind() == Kind.Int
            && n.GetLong() == 1L
            && l.IncompleteKind() == Kind.List
            && l.Len() is { } len
            && len.IncompleteKind() == Kind.Int
            && len.IsConcrete())
        {
            var branchCount = len.GetLong();
            var branches = new List<Value>();

            for (long i = 0; i < branchCount; i++)
            {
                branches.Add(l.Lookup($"[{i}]"));
            }

            return branches;
        }

        foreach (var v in expr.Values)
        {
            v.Dispose();
        }

        return null;
    }

    private CueStructValue VisitStruct(Value value)
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

    private CueListValue VisitList(Value value)
    {
        var path = value.Path();

        using var elementValue = value.LookupAnyIndex();
        var elementType = Visit(elementValue);

        return new CueListValue(path, elementType);
    }

    private CueDisjunction VisitDisjunction(Value value, IEnumerable<Value> branches)
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

    private static (string? name, Dictionary<string, string> branches) FindDiscriminatorField(List<CueValueNode> branches)
    {
        if (branches.Count == 0 || branches.Any(b => b is not CueStructValue))
        {
            return (null, []);
        }

        var structBranches = branches.Cast<CueStructValue>().ToList();

        var namesPerBranch = structBranches
            .Select(e => e.Fields
                .Where(f => f.Value is CueStringValue
                {
                    ConcreteValue: not null
                })
                .Select(f => new
                {
                    f.Name,
                    Value = (CueStringValue)f.Value
                }))
            .ToArray();

        var fields = namesPerBranch
            .Aggregate((a, b) =>
                a.IntersectBy(b.Select(e => e.Name), e => e.Name))
            .ToArray();

        var name = fields
            .Select(field => new
            {
                field,
                allValues = structBranches
                    .Select(b => GetDiscriminatorValue(b, field.Name))
                    .ToArray()
            })
            .Where(t =>
                t.allValues.Distinct().Count() == t.allValues.Length)
            .Select(t => t.field.Name)
            .FirstOrDefault();

        if (name == null)
        {
            return (null, []);
        }

        var branchDict = structBranches.ToDictionary(
            b => GetDiscriminatorValue(b, name),
            b => b.Path);

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

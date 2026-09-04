using Cuelang.Cue;

namespace Cue.Generator;

public sealed class CueValueVisitor(Value[] rootDefinitions, TextWriter? writer)
{
    private readonly HashSet<string> _definedPaths = [];

    private static string FormatExpr(Value value)
    {
        var expr = value.Expr();

        var call = expr.Op == ExprOp.Call ? $"Call<${expr.CallName}>" : expr.Op.ToString();

        if (expr.Op is ExprOp.No)
        {
            if (value.IsConcrete())
            {
                try
                {
                    return value.GetJson();
                }
                catch
                {
                    return $"???<{value.Path()}: {value.Kind()}>";
                }
            }

            return $"(No ???<{expr.Values[0].Path()}: {expr.Values[0].IncompleteKind()}>)";
        }

        return $"({call} {string.Join(" ", expr.Values.Select(FormatExpr))})";
    }


    public static IEnumerable<CueValueNode> VisitRoot(Value value, TextWriter? debug = null)
    {
        var definitions = value.Fields(new EvalOption.Definitions(true));

        try
        {
            var visitor = new CueValueVisitor(definitions, debug);
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
        return new CueValueVisitor([], null).Visit(value);
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
        writer?.WriteLine($"DEBUG LIST LENGTH {value.Path()}: {FormatExpr(value)}");

        foreach (var rootValue in rootDefinitions)
        {
            if (Value.SchemaComparer.Equals(value, rootValue) && _definedPaths.Contains(rootValue.Path()))
            {
                return new CueDefinitionReference(rootValue.Path());
            }
        }

        _definedPaths.Add(value.Path());
        
        if (value.Expr().Op == ExprOp.Selector)
        {
            return new CueDefinitionReference(value.Path());
        }

        var kind = value.IncompleteKind();

        if (kind is Kind.Top or Kind.Struct && DisjunctionBranches(value) is { } branches)
        {
            var disjunction = VisitDisjunction(value, branches);
            return disjunction.Branches switch
            {
                [CueNullValue, CueNullValue] => new CueNullable(new CueBottomValue(value.Path())),
                [CueNullValue, var a] => new CueNullable(a),
                [var b, CueNullValue] => new CueNullable(b),
                _ => disjunction
            };
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
        var count = GetConcreteElementCount(value);

        var elements = Enumerable.Range(0, (int)count)
            .Select(index =>
            {
                using var element = value.Lookup($"[{index}]");
                return Visit(element);
            })
            .ToList();

        Value? anyIndex;
        try
        {
            anyIndex = value.LookupAnyIndex();
        }
        catch (CueError)
        {
            anyIndex = null;
        }

        return new CueListValue(path, anyIndex is { } v ? Visit(v) : null, elements);
    }

    private static long GetConcreteElementCount(Value value)
    {
        using var len = value.Len();

        if (len.IncompleteKind() != Kind.Int)
        {
            throw new InvalidDataException("List length must be an int.");
        }

        if (len.IsConcrete())
        {
            return len.GetLong();
        }

        try
        {
            var lb = len.LowerBound();
            return (int) lb!.Value;
        }
        finally
        {
            foreach (var expressionValue in len.Expr().Values)
            {
                expressionValue.Dispose();
            }
        }
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

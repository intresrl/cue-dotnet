using System.Numerics;
using Cuelang.Cue;
using ExtendedNumerics;

namespace Cue.Generator;

public sealed class CueValueVisitor(Value[] rootDefinitions, TextWriter? writer, ICueExprVisitor cueExprVisitor)
{
    private readonly HashSet<string> _definedPaths = [];

    public static IEnumerable<CueValueNode> VisitRoot(Value value, TextWriter? debug = null)
    {
        var definitions = value.Fields(new EvalOption.Definitions(true));

        try
        {
            var visitor = new CueValueVisitor(definitions, debug, new CueExprVisitor(debug));
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
        return new CueValueVisitor([], null, new CueExprVisitor(null)).Visit(value);
    }

    public CueValueNode Visit(Value value)
    {
        writer?.WriteLine($"DEBUG LIST LENGTH {value.Path()}: {value.FormatExpr()}");

        foreach (var rootValue in rootDefinitions)
        {
            if (Value.SchemaComparer.Equals(value, rootValue) && _definedPaths.Contains(rootValue.Path()))
            {
                return new CueDefinitionReference(rootValue.Path());
            }
        }

        _definedPaths.Add(value.Path());
        
        if (value.Expr() is { Op: ExprOp.Selector, Values: [_, { } schemaValue] } && schemaValue.Kind() == Kind.String)
        {
            return new CueDefinitionReference(schemaValue.GetString()!);
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

        var concrete = GetConcreteValue(value);
        var constraint = ExtractConstraint(value);

        return kind switch
        {
            Kind.Bottom => new CueBottomValue(value.Path()),
            Kind.Null => new CueNullValue(value.Path()),
            Kind.Number => new CueNumberValue(value.Path()),
            Kind.Top => new CueTopValue(value.Path()),

            Kind.Bool => new CueBoolValue(value.Path(), concrete as bool?, constraint),
            Kind.Int => new CueIntValue(value.Path(), concrete as BigInteger?, constraint),
            Kind.Float => new CueFloatValue(value.Path(), concrete as BigDecimal?, constraint),
            Kind.String => new CueStringValue(value.Path(), concrete as string, constraint),
            Kind.Bytes => new CueBytesValue(value.Path(), concrete as byte[]),

            Kind.Struct => VisitStruct(value),
            Kind.List => VisitList(value),

            _ => throw new ArgumentOutOfRangeException(nameof(kind), kind, "Unexpected kind")
        };
    }

    private static object? GetConcreteValue(Value value)
    {
        if (!value.IsConcrete())
        {
            return null;
        }

        return value.Kind() switch
        {
            Kind.Bool => value.GetBoolean(),
            Kind.Int => value.GetFloat().ToBigInteger(),
            Kind.Float => value.GetFloat().ToBigDecimal(),
            Kind.String => value.GetString(),
            Kind.Bytes => value.GetBytes(),
            _ => null
        };
    }

    private CueExpr? ExtractConstraint(Value value)
    {
        var expr = value.Expr();
        
        // If the value is concrete and has no expression (ExprOp.No), create an equality constraint
        if (expr.Op == ExprOp.No && value.IsConcrete())
        {
            return CreateConcreteConstraint(value);
        }

        try
        {
            return cueExprVisitor.Visit(value);
        }
        catch
        {
            return null;
        }
    }

    private static CueExpr? CreateConcreteConstraint(Value value)
    {
        return value.Kind() switch
        {
            Kind.Int => new CueUnaryExpr(CueUnaryExpr.Op.Equal, new CueIntegerExpr(value.GetFloat().ToBigInteger())),
            Kind.Float => new CueUnaryExpr(CueUnaryExpr.Op.Equal, new CueFloatExpr(value.GetFloat().ToBigDecimal())),
            Kind.String => new CueUnaryExpr(CueUnaryExpr.Op.Equal, new CueStringExpr(value.GetString()!)),
            Kind.Bool => new CueUnaryExpr(CueUnaryExpr.Op.Equal, new CueBoolExpr(value.GetBoolean())),
            _ => null
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
        
        writer?.WriteLine($"{path} list concrete length: {count}");

        var elements = Enumerable.Range(0, count)
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

    private int GetConcreteElementCount(Value value)
    {
        using var len = value.Len();

        if (len.IncompleteKind() != Kind.Int)
        {
            throw new InvalidDataException("List length must be an int.");
        }

        if (len.IsConcrete())
        {
            return (int) len.GetLong();
        }

        try
        {
            var expr = cueExprVisitor.Visit(len);
            var lb = expr.Bounds().Lower ?? BigInteger.Zero;
            return (int) lb;
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
                .Where(f => f.Value is CueStringValue { ConcreteValue: not null })
                .Select(f => new { f.Name, Value = (CueStringValue)f.Value }))
            .ToArray();

        var fields = namesPerBranch
            .Aggregate((a, b) => a.IntersectBy(b.Select(e => e.Name), e => e.Name))
            .ToArray();

        var name = fields
            .Select(field => new
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
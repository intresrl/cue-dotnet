using Cue.Generator.Roslyn;
using Cuelang.Cue;

namespace Cue.Generator;

public sealed class NewCueValueVisitor
{
    public abstract record Node(Value? Value);
    public sealed record AnyLeaf(Value? Value, bool Null) : Node(Value);
    public sealed record Leaf(Value Value) : Node(Value);
    public sealed record Struct(Value Value, IEnumerable<Named> Fields) : Node(Value);
    public sealed record Named(string Name, Node InnerValue) : Node((Value?) null);
    public sealed record List(Value Value, Value Element) : Node(Value);
    public sealed record Nullable(Node Inner) : Node((Value?) null);
    public sealed record Disjunction(Value Value, IEnumerable<Value> Branches) : Node(Value);

    public static IEnumerable<Node> Visit(Value root)
    {
        var definitions = root.Fields(new EvalOption.Definitions(true));


        return definitions
            .BreadthFirstSearch(Select, Value.SchemaComparer)
            .Select(e => new Named(root.Path(), e));
    }

    private static (IEnumerable<Value> Members, IEnumerable<Node> Results) Select(Value value) =>
        value.IncompleteKind() switch
        {
            Kind.Top or Kind.Struct when DisjunctionBranches(value) is { } branches => VisitDisjunction(value, branches),
            Kind.Struct => VisitStruct(value),
            Kind.List => VisitList(value),
            Kind.Top => ([], [new AnyLeaf(value, false)]),
            Kind.Null => ([], [new AnyLeaf(value, true)]),
            _ => ([], [new Leaf(value)])
        };

    private static (IEnumerable<Value> Members, IEnumerable<Node> Results) VisitDisjunction(
        Value value,
        IEnumerable<Value> branches)
    {
        var branchArray = branches.ToArray();
        if (branchArray.Length == 0)
        {
            throw new InvalidOperationException("disjunction has 0 branches");
        }

        var byNullability = branchArray
            .GroupBy(branch => branch.IncompleteKind() == Kind.Null)
            .ToDictionary(e => e.Key, e => e.ToArray());
        
        try
        {
            var nonNullBranches = byNullability.GetValueOrDefault(false) ?? [];

            // if all is null, return a null leaf node with no Value to free (we free all values here)
            if (nonNullBranches.Length == 0)
            {
                return ([], [new AnyLeaf(null, true)]);
                // free all in branchArray
            }

            // if all are non-null branches, plain disjunction
            if (nonNullBranches.Length == branchArray.Length)
            {
                return (nonNullBranches, [new Disjunction(value, nonNullBranches)]);
            }

            // if some null branches, disjunction wrapped in nullable
            if (nonNullBranches.Length != 1)
            {
                return (nonNullBranches, [new Nullable(new Disjunction(value, nonNullBranches))]);
            }

            // if only one non-null branch, nullable non-null branch
            var (continuations, results) = Select(nonNullBranches[0]);
            return (continuations, results.Select(e => new Nullable(e)));
        }
        finally
        {
            // free null branches immediately
            foreach (var b in byNullability.GetValueOrDefault(true) ?? [])
            {
                b.Dispose();
            }
        }
    }

    private static (IEnumerable<Value> Members, IEnumerable<Node> Results) VisitStruct(Value value)
    {
        var path = value.Path();
        var values = value.Fields(new EvalOption.Optionals(true));

        var fields = values
            .Select(field => new Named(GetFieldName(path, field.Path()), field))
            .ToArray();

        return (values, [new Struct(value, fields)]);
    }

    private static (IEnumerable<Value> Members, IEnumerable<Node> Results) VisitList(Value value)
    {
        var element = value.LookupAnyIndex();

        return ([element], [new List(value, element)]);
    }

    private static IEnumerable<Value>? DisjunctionBranches(Value value)
    {
        var expr = value.Expr();

        if (expr.Op == ExprOp.Or) return expr.Values;

        if (expr is
            {
                Op: ExprOp.Call,
                CallName: "matchN",
                Values: [{ } n, { } list]
            }
            && n.IncompleteKind() == Kind.Int
            && n.GetLong() == 1
            && list.IncompleteKind() == Kind.List
            && list.Len() is { } len
            && len.IncompleteKind() == Kind.Int
            && len.IsConcrete())
            return Enumerable
                .Range(0, checked((int)len.GetLong()))
                .Select(i => list.Lookup($"[{i}]"))
                .ToArray();

        foreach (var child in expr.Values) child.Dispose();

        return null;
    }

    private static string GetFieldName(string parentPath, string childPath)
    {
        if (string.IsNullOrEmpty(parentPath)) return childPath;

        var prefix = parentPath + ".";

        return childPath.StartsWith(prefix, StringComparison.Ordinal)
            ? childPath[prefix.Length..]
            : childPath;
    }
}
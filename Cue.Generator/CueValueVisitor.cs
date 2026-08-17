using Cuelang.Cue;

namespace Cue.Generator;

public abstract class CueValueVisitor<TResult>
{
    public TResult Visit(Value value)
    {
        // For non-concrete declarations (schemas), prefer the incomplete kind
        var kind = value.Kind() is Kind.Bottom ? value.IncompleteKind() : value.Kind();
        return Dispatch(value, kind);
    }

    private TResult Dispatch(Value value, Kind kind)
    {
        if (kind is Kind.Top or Kind.Struct && DisjunctionBranches(value) is { } branches)
        {
            return VisitDisjunction(value, branches);
        }

        // Delegate to base class incomplete kind dispatch
        var visitKind = kind == Kind.Top ? value.IncompleteKind() : kind;

        return visitKind switch
        {
            Kind.Bottom => VisitBottom(value),
            Kind.Null => VisitNull(value),
            Kind.Bool => VisitBool(value),
            Kind.Int => VisitInt(value),
            Kind.Float => VisitFloat(value),
            Kind.String => VisitString(value),
            Kind.Bytes => VisitBytes(value),
            Kind.Number => VisitNumber(value),
            Kind.Top => VisitTop(value),
            Kind.Struct => VisitStruct(value),
            Kind.List => VisitList(value),
            _ => throw new ArgumentOutOfRangeException(nameof(kind), kind, "Unexpected kind")
        };
    }
    
    private static IEnumerable<Value>? DisjunctionBranches(Value value)
    {
        // Check for disjunctions at the top level
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
            && n.Kind() == Kind.Int && n.GetLong() == 1L
            && l.Kind() == Kind.List && l.Len() is { } len && len.Kind() == Kind.Int && len.IsConcrete()
           )
        {
            var branches = new List<Value>();
            var branchCount = len.GetLong();
            
            for (long i = 0; i < branchCount; i++)
            {
                branches.Add(l.Lookup($"[{i}]"));
            }

            return branches;
        }
        
        // we don't care about this expression, dispose values
        foreach (var v in expr.Values)
        {
            v.Dispose();
        }
        return null;
    }

    protected abstract TResult VisitBottom(Value value);

    protected abstract TResult VisitNull(Value value);

    protected abstract TResult VisitBool(Value value);

    protected abstract TResult VisitInt(Value value);

    protected abstract TResult VisitFloat(Value value);

    protected abstract TResult VisitString(Value value);

    protected abstract TResult VisitBytes(Value value);

    protected abstract TResult VisitNumber(Value value);

    protected abstract TResult VisitTop(Value value);

    protected abstract TResult VisitStruct(Value value);

    protected abstract TResult VisitList(Value value);
    
    protected abstract TResult VisitDisjunction(Value value, IEnumerable<Value> branches);
}

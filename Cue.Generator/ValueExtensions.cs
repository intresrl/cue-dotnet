using Cuelang.Cue;

namespace Cue.Generator;

public static class ValueExtensions
{
    public static string FormatExpr(this Value value)
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
}
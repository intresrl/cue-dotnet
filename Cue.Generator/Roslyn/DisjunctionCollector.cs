namespace Cue.Generator.Roslyn;

public class DisjunctionCollector
{
    private readonly Dictionary<CueDisjunction, CueDisjunction> _disjunctions = new(new CueValueNodeComparer());

    public (IReadOnlyList<CueValueNode>, IReadOnlyList<CueDisjunction>) Visit(IEnumerable<CueValueNode> nodes)
    {
        var nodeArray = nodes.ToArray();

        var allDisjunctions = nodeArray
            .BreadthFirstSearch<CueDisjunction>(n => n switch
            {
                CueDisjunction d => (d.Branches, [d]),
                CueStructValue s => (s.Fields.Select(f => f.Value), []),
                CueListValue l => ([l.ElementType], []),
                _ => ([], [])
            })
            .ToArray();

        foreach (var d in allDisjunctions) _disjunctions.TryAdd(d, d);

        return (nodeArray.Select(Visit).ToArray(), _disjunctions.Keys.ToArray());
    }

    private CueValueNode Visit(CueValueNode node)
    {
        return node switch
        {
            CueBottomValue or CueTopValue or CueTopValue or CueNullValue => node,

            CueStructValue value =>
                new CueStructValue(
                    value.Path,
                    value.Fields
                        .Select(field => field with { Value = Visit(field.Value) })
                        .ToArray()),

            CueListValue value =>
                new CueListValue(
                    value.Path,
                    Visit(value.ElementType)),

            CueDisjunction d => new CueDefinitionReference(_disjunctions[d].Path),

            // be careful about these in the future. They will contain constraints potentially
            _ => node
        };
    }
}
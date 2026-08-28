namespace Cue.Generator.Roslyn;

public static class SearchAlgorithms
{
    public static IEnumerable<T> BreadthFirstSearch<T>(
        this CueValueNode root,
        Func<CueValueNode, (IEnumerable<CueValueNode> Members, IEnumerable<T> Results)> selector)
        => BreadthFirstSearch([root], selector);
    
    public static IEnumerable<T> BreadthFirstSearch<T>(
        this IEnumerable<CueValueNode> root,
        Func<CueValueNode, (IEnumerable<CueValueNode> Members, IEnumerable<T> Results)> selector)
    {
        var visitedStructPaths = new HashSet<string>(StringComparer.Ordinal);

        var queue = new Queue<CueValueNode>();
        foreach (var node in root)
        {
            queue.Enqueue(node);
        }

        while (queue.TryDequeue(out var node))
        {
            // a cueNullable node has the same path as its inner value. skip duplication check here
            // TODO: in the future here use either reference equality on CueValueNode or refactor this to use memoized
            //  Value comparison when refactoring to thin layer of CueValue values
            if (node is not CueNullable && !visitedStructPaths.Add(node.Path))
            {
                continue;
            }

            var (members, results) = selector(node);

            foreach (var member in members)
            {
                queue.Enqueue(member);
            }

            foreach (var result in results)
            {
                yield return result;
            }
        }
    }
}
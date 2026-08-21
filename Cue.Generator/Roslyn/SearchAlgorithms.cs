namespace Cue.Generator.Roslyn;

public static class SearchAlgorithms
{
    public static IEnumerable<T> BreadthFirstSearch<T>(
        this CueValueNode root,
        Func<CueValueNode, (IEnumerable<CueValueNode> Members, IEnumerable<T> Results)> selector)
    {
        return BreadthFirstSearch([root], selector);
    }

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
            if (!visitedStructPaths.Add(node.Path))
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
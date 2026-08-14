namespace Cue.Generator.Roslyn;

public static class SearchAlgorithms
{
    public static IEnumerable<T> BreadthFirstSearch<T>(
        this CueValueNode root, 
        Func<CueValueNode, (IEnumerable<CueValueNode> Members, IEnumerable<T> Results)> selector)
    {
        var visitedStructPaths = new HashSet<string>(StringComparer.Ordinal);
        
        var queue = new Queue<CueValueNode>();
        queue.Enqueue(root);

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
using System.Runtime.CompilerServices;

namespace Cue.Generator.Roslyn;

public static class SearchAlgorithms
{
    public static IEnumerable<TResult> BreadthFirstSearch<TNode, TResult>(
        this IEnumerable<TNode> root,
        Func<TNode, (IEnumerable<TNode> Members, IEnumerable<TResult> Results)> selector,
        IEqualityComparer<TNode>? comparer = null)
        where TNode : notnull
    {
        var visited = new HashSet<TNode>(comparer);
        var queue = new Queue<TNode>(root);

        while (queue.TryDequeue(out var node))
        {
            if (!visited.Add(node))
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

    public static IEnumerable<T> BreadthFirstSearch<T>(
        this IEnumerable<CueValueNode> root,
        Func<CueValueNode, (IEnumerable<CueValueNode> Members, IEnumerable<T> Results)> selector)
    {
        return root.BreadthFirstSearch(selector, CueValueNodePathComparer.Instance);
    }

    private sealed class CueValueNodePathComparer : IEqualityComparer<CueValueNode>
    {
        public static readonly CueValueNodePathComparer Instance = new();

        public bool Equals(CueValueNode? x, CueValueNode? y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (x is null || y is null) return false;

            // nullable nodes are never considered already visited
            if (x is CueNullable || y is CueNullable) return false;

            return StringComparer.Ordinal.Equals(x.Path, y.Path);
        }

        public int GetHashCode(CueValueNode obj)
        {
            return obj is CueNullable
                ? RuntimeHelpers.GetHashCode(obj)
                : StringComparer.Ordinal.GetHashCode(obj.Path);
        }
    }
}
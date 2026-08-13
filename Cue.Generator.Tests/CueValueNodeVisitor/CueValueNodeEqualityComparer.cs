using Xunit.Sdk;

public sealed class CueValueNodeComparer(Func<CueValueNode, string> source) : IAssertEqualityComparer<CueValueNode>
{
    public bool Equals(CueValueNode? x, CueValueNode? y)
    {
        return x is null || y is null
            ? x is null && y is null
            : TreesIdentical(x, y).Identical;
    }

    public int GetHashCode(CueValueNode obj) => obj.GetHashCode();

    public AssertEqualityResult Equals(
        CueValueNode? x,
        CollectionTracker? xTracker,
        CueValueNode? y,
        CollectionTracker? yTracker)
    {
        if (x is null || y is null)
            return AssertEqualityResult.ForResult(x is null && y is null, x, y);

        var (identical, differencePath) = TreesIdentical(x, y);

        if (identical)
            return AssertEqualityResult.ForResult(true, x, y);

        return AssertEqualityResult.ForResult(
            false,
            x,
            y,
            new CueValueNodeMismatchException(
                $"Trees differ at: {differencePath}\n\n" +
                $"Expected:\n{source(x)}\n\n" +
                $"Actual:\n{source(y)}"));
    }

    private sealed class CueValueNodeMismatchException(string message) : Exception(message);

    private static (bool Identical, string? DifferencePath) TreesIdentical(
        CueValueNode left,
        CueValueNode right)
    {
        var stack = new Stack<(CueValueNode Left, CueValueNode Right)>();
        stack.Push((left, right));

        while (stack.Count > 0)
        {
            var (x, y) = stack.Pop();

            if (x.GetType() != y.GetType() || x.Path != y.Path)
                return (false, x.Path);

            switch (x, y)
            {
                case (CueBottomValue, CueBottomValue):
                case (CueTopValue, CueTopValue):
                case (CueNullValue, CueNullValue):
                case (CueNumberValue, CueNumberValue):
                    break;

                case (CueBoolValue a, CueBoolValue b):
                    if (a.ConcreteValue != b.ConcreteValue)
                        return (false, x.Path);
                    break;

                case (CueIntValue a, CueIntValue b):
                    if (a.ConcreteValue != b.ConcreteValue)
                        return (false, x.Path);
                    break;

                case (CueFloatValue a, CueFloatValue b):
                    if (!a.ConcreteValue.Equals(b.ConcreteValue))
                        return (false, x.Path);
                    break;

                case (CueStringValue a, CueStringValue b):
                    if (a.ConcreteValue != b.ConcreteValue)
                        return (false, x.Path);
                    break;

                case (
                    CueBytesValue { ConcreteValue: var a },
                    CueBytesValue { ConcreteValue: var b }):

                    if (a is null && b is null)
                        break;

                    if (a is null || b is null || !a.SequenceEqual(b))
                        return (false, x.Path);

                    break;

                case (CueStructValue a, CueStructValue b):
                    if (a.Fields.Count != b.Fields.Count)
                        return (false, x.Path);

                    // Reverse push so fields are compared in their original order.
                    for (var i = a.Fields.Count - 1; i >= 0; i--)
                    {
                        var leftField = a.Fields[i];
                        var rightField = b.Fields[i];

                        if (leftField.Name != rightField.Name || leftField.Optional != rightField.Optional)
                            return (false, leftField.Value.Path);

                        stack.Push((leftField.Value, rightField.Value));
                    }

                    break;

                case (CueListValue a, CueListValue b):
                    stack.Push((a.ElementType, b.ElementType));
                    break;

                case (CueDisjunction a, CueDisjunction b):
                    if (a.Branches.Count != b.Branches.Count)
                        return (false, x.Path);

                    for (var i = a.Branches.Count - 1; i >= 0; i--)
                        stack.Push((a.Branches[i], b.Branches[i]));

                    break;

                default:
                    return (false, x.Path);
            }
        }

        return (true, null);
    }
}
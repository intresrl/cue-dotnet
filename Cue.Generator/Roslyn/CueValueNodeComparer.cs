namespace Cue.Generator.Roslyn;

public class CueValueNodeComparer : IEqualityComparer<CueValueNode>
{
    public bool Equals(CueValueNode? x, CueValueNode? y)
    {
        if (ReferenceEquals(x, y)) return true;
        if (x is null || y is null) return false;

        if (x is CueDefinitionReference xRef)
            return y is CueDefinitionReference yRef
                ? xRef.Definition == yRef.Definition
                : xRef.Definition == y.Path;

        if (y is CueDefinitionReference yOtherRef) return yOtherRef.Definition == x.Path;

        if (x is CueBottomValue or CueTopValue or CueNullValue or CueNumberValue) return true;

        return (x, y) switch
        {
            (CueBoolValue a, CueBoolValue b) => a.ConcreteValue == b.ConcreteValue,
            (CueIntValue a, CueIntValue b) => a.ConcreteValue == b.ConcreteValue,
            (CueFloatValue a, CueFloatValue b) => a.ConcreteValue == b.ConcreteValue,
            (CueStringValue a, CueStringValue b) => a.ConcreteValue == b.ConcreteValue,
            (CueBytesValue a, CueBytesValue b) => ByteArraysEqual(a.ConcreteValue, b.ConcreteValue),
            (CueListValue a, CueListValue b) => Equals(a.ElementType, b.ElementType),
            (CueStructValue a, CueStructValue b) => StructsEqual(a, b),
            (CueDisjunction a, CueDisjunction b) => DisjunctionsEqual(a, b),
            _ => false
        };
    }

    public int GetHashCode(CueValueNode obj)
    {
        var hash = new HashCode();
        hash.Add(obj.GetType());

        switch (obj)
        {
            case CueBottomValue:
            case CueTopValue:
            case CueNullValue:
            case CueNumberValue:
                break;

            case CueBoolValue value:
                hash.Add(value.ConcreteValue);
                break;

            case CueIntValue value:
                hash.Add(value.ConcreteValue);
                break;

            case CueFloatValue value:
                hash.Add(value.ConcreteValue);
                break;

            case CueStringValue value:
                hash.Add(value.ConcreteValue);
                break;

            case CueBytesValue value:
                if (value.ConcreteValue is not null)
                    foreach (var b in value.ConcreteValue)
                        hash.Add(b);

                break;

            case CueDefinitionReference reference:
                hash.Add(reference.Definition);
                break;

            case CueListValue l:
                hash.Add(GetHashCode(l.ElementType));
                break;

            case CueStructValue s:
                foreach (var field in s.Fields)
                {
                    hash.Add(field.Name);
                    hash.Add(field.Optional);
                    hash.Add(GetHashCode(field.Value));
                }

                break;

            case CueDisjunction disjunction:
                hash.Add(disjunction.DiscriminatorField);

                foreach (var branch in disjunction.Branches) hash.Add(GetHashCode(branch));

                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(obj));
        }

        return hash.ToHashCode();
    }

    private bool StructsEqual(CueStructValue x, CueStructValue y)
    {
        if (x.Fields.Count != y.Fields.Count) return false;

        for (var i = 0; i < x.Fields.Count; i++)
        {
            var a = x.Fields[i];
            var b = y.Fields[i];

            if (a.Name != b.Name ||
                a.Optional != b.Optional ||
                !Equals(a.Value, b.Value))
                return false;
        }

        return true;
    }

    private bool DisjunctionsEqual(CueDisjunction x, CueDisjunction y)
    {
        if (x.DiscriminatorField != y.DiscriminatorField || x.Branches.Count != y.Branches.Count) return false;

        return !x.Branches.Where((t, i) => !Equals(t, y.Branches[i])).Any();
    }

    private static bool ByteArraysEqual(byte[]? x, byte[]? y)
    {
        if (ReferenceEquals(x, y)) return true;
        if (x is null || y is null) return false;

        return x.AsSpan().SequenceEqual(y);
    }
}
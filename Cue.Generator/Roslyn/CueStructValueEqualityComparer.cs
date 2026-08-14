namespace Cue.Generator.Roslyn;

internal sealed class CueStructValueEqualityComparer : IEqualityComparer<CueStructValue>
{
    public bool Equals(CueStructValue? x, CueStructValue? y)
    {
        if (ReferenceEquals(x, y))
            return true;

        if (x is null || y is null)
            return false;

        if (x.Fields.Count != y.Fields.Count)
            return false;

        var yFields = y.Fields.ToDictionary(
            f => f.Name,
            f => f.Value.GetType());

        return x.Fields.All(field =>
            yFields.TryGetValue(field.Name, out var type) &&
            field.Value.GetType() == type);
    }

    public int GetHashCode(CueStructValue obj)
    {
        var hash = new HashCode();

        foreach (var field in obj.Fields.OrderBy(f => f.Name))
        {
            hash.Add(field.Name);
            hash.Add(field.Value.GetType());
        }

        return hash.ToHashCode();
    }
}
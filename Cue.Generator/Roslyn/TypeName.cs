using System.Runtime.CompilerServices;

namespace Cue.Generator.Roslyn;

public enum NamingKind
{
    Type,
    Disjunction,
    DisjunctionBranch
}

[InterpolatedStringHandler]
public sealed class TypeName
{
    private readonly List<IPart> _parts = [];

    public TypeName(int literalLength, int formattedCount)
    {
    }

    private TypeName(IEnumerable<IPart> parts)
    {
        _parts.AddRange(parts);
    }

    public static TypeName FromRef(string path, NamingKind kind) => new([new Ref(path, kind)]);

    public static TypeName Join(TypeName separator, IEnumerable<TypeName> names)
    {
        var nameArray = names.ToArray();
        
        var parts = nameArray
            .SelectMany(IEnumerable<TypeName> (e, i) => i == nameArray.Length - 1 ? [e] : [e, separator])
            .SelectMany(e => e._parts);

        return new TypeName(parts);
    }

    public void AppendLiteral(string value)
    {
        if (value.Length != 0)
        {
            _parts.Add(new Literal(value));
        }
    }

    public void AppendFormatted(TypeName value)
    {
        _parts.Add(new Nested(value));
    }

    public string Format(Func<string, NamingKind, string> formatter)
    {
        return string.Join("", FormattedParts(formatter));
    }

    private IEnumerable<string> FormattedParts(Func<string, NamingKind, string> formatter)
    {
        return _parts
            .SelectMany(p => p switch
            {
                Literal l => [l.Value],
                Ref r => [formatter(r.Path, r.Kind)],
                Nested n => n.Value.FormattedParts(formatter),

                _ => throw new InvalidOperationException()
            });
    }

    private interface IPart;
    private sealed record Literal(string Value) : IPart;
    private sealed record Ref(string Path, NamingKind Kind) : IPart;
    private sealed record Nested(TypeName Value) : IPart;
}
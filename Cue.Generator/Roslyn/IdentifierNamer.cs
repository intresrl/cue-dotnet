using System.Text.RegularExpressions;

namespace Cue.Generator.Roslyn;

public interface IIdentifierNamer
{
    string TypeName(string path, NamingKind kind);
    string Identifier(string name);
}

public class IdentifierNamer : IIdentifierNamer
{
    private static int _anonymousIndex = 1;

    private static string TypeName(string path)
    {
        if (string.IsNullOrEmpty(path)) return "Root";

        // Remove dots and indexers
        var name = path
            .Replace("[_]", "Any")
            .Replace("[", string.Empty)
            .Replace("]", string.Empty)
            .Replace(".", string.Empty);

        return ToPascalCase(SanitizeIdentifier(name));
    }

    public string TypeName(string path, NamingKind kind)
    {
        return kind switch
        {
            NamingKind.Type => TypeName(path),
            NamingKind.Disjunction => TypeName(path) + "Base",
            NamingKind.DisjunctionBranch => "As" + TypeName(path),
            _ => throw new ArgumentOutOfRangeException(nameof(kind), kind, null)
        };
    }

    public string Identifier(string name)
    {
        return ToPascalCase(SanitizeIdentifier(name));
    }

    private static string ToPascalCase(string s)
    {
        var parts = s.Split(['_', '-'], StringSplitOptions.RemoveEmptyEntries);
        return string.Concat(parts.Select(p => char.ToUpperInvariant(p[0]) + p[1..]));
    }

    private static string SanitizeIdentifier(string s)
    {
        if (string.IsNullOrEmpty(s)) return "Anonymous" + _anonymousIndex++;

        var chars = s.Where(ch => char.IsLetterOrDigit(ch) || ch == '_').ToArray();
        var res = new string(chars);

        if (char.IsDigit(res.FirstOrDefault()))
        {
            res = "_" + res;
        }

        return res;
    }
}
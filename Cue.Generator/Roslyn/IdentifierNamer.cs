using System.Text.RegularExpressions;

namespace Cue.Generator.Roslyn;

public interface IIdentifierNamer
{
    string BaseClassName(string path);
    string TypeName(string path);
    string Identifier(string name);
}

public partial class IdentifierNamer : IIdentifierNamer
{
    private static int _anonymousIndex = 1;

    public string BaseClassName(string path)
    {
        // Generate name like "ValueFormatBase" from a discriminator union path
        var typeName = TypeName(path);
        return typeName + "Base";
    }

    public string TypeName(string path)
    {
        if (string.IsNullOrEmpty(path)) return "Root";

        // Remove dots and indexers
        var name = Indexer().Replace(path, string.Empty)
            .Replace(".", string.Empty);

        return ToPascalCase(SanitizeIdentifier(name));
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

    [GeneratedRegex(@"\[[^\]]*\]")]
    private static partial Regex Indexer();
}
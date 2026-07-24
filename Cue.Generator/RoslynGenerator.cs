using Cuelang.Cue;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Cue.Generator;

public sealed class RoslynGenerator
{
    // map from struct path -> generated type name
    private readonly Dictionary<string, string> _typeNames = new();

    public string GenerateCode(CueValueNode root)
    {
        // collect struct nodes and assign type names
        CollectStructs(root);
        
        var compilationUnit = SyntaxFactory.CompilationUnit()
            .AddUsings(
                SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System")),
                SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System.Collections.Generic"))
            );

        var members = new List<MemberDeclarationSyntax>();

        // create classes for each struct (keep deterministic order)
        foreach (var (path, typeName) in _typeNames.OrderBy(kv => kv.Value))
        {
            if (!TryFindStruct(root, path, out var structNode)) continue;
            var classDecl = CreateClassDeclaration(typeName, structNode!);
            members.Add(classDecl);
        }

        compilationUnit = compilationUnit.AddMembers([.. members]);
        return compilationUnit.NormalizeWhitespace().ToFullString();
    }

    private void CollectStructs(CueValueNode node)
    {
        CollectStructs(node, new HashSet<string>(StringComparer.Ordinal));
    }

    private void CollectStructs(CueValueNode node, HashSet<string> visitedStructPaths)
    {
        switch (node)
        {
            case CueStructValue s:
            {
                Console.WriteLine(s.Path);

                // Avoid revisiting the same struct path to prevent infinite recursion on self-references
                if (!visitedStructPaths.Add(s.Path)) return;

                var typeName = GenerateTypeName(s.Path);
                _typeNames.TryAdd(s.Path, typeName);

                foreach (var f in s.Fields) CollectStructs(f.Value, visitedStructPaths);

                break;
            }
            case CueListValue l:
            {
                CollectStructs(l.ElementType, visitedStructPaths);
                break;
            }
        }
    }

    private static bool TryFindStruct(CueValueNode root, string path, out CueStructValue? result)
    {
        result = null;
        switch (root)
        {
            case CueStructValue s when s.Path == path:
                result = s;
                return true;
            case CueStructValue sv:
            {
                foreach (var f in sv.Fields)
                    if (TryFindStruct(f.Value, path, out result))
                        return true;
                break;
            }
            case CueListValue lv:
            {
                if (TryFindStruct(lv.ElementType, path, out result))
                    return true;
                break;
            }
        }

        return false;
    }

    private static string GenerateTypeName(string path)
    {
        if (string.IsNullOrEmpty(path)) return "Root";
        // pick last meaningful segment
        var seg = path.Split('.', StringSplitOptions.RemoveEmptyEntries).Last();
        // remove indexers like scores[1]
        var idx = seg.IndexOf('[');
        if (idx >= 0) seg = seg[..idx];
        var typeName = ToPascalCase(SanitizeIdentifier(seg));
        return typeName;
    }

    private static string SanitizeIdentifier(string s)
    {
        if (string.IsNullOrEmpty(s)) return "Item";
        // remove invalid chars
        var chars = s.Where(ch => char.IsLetterOrDigit(ch) || ch == '_').ToArray();
        var res = new string(chars);
        if (char.IsDigit(res.FirstOrDefault())) res = "_" + res;
        return res;
    }

    private static string ToPascalCase(string s)
    {
        var parts = s.Split(['_', '-'], StringSplitOptions.RemoveEmptyEntries);
        return string.Concat(parts.Select(p => char.ToUpperInvariant(p[0]) + p[1..]));
    }

    private ClassDeclarationSyntax CreateClassDeclaration(string typeName, CueStructValue node)
    {
        return SyntaxFactory.ClassDeclaration(typeName)
            .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
            .AddMembers([
                .. node.Fields.Select(DeclareProperty)
            ]);
    }

    private PropertyDeclarationSyntax DeclareProperty(CueStructField field)
    {
        var propName = ToPascalCase(SanitizeIdentifier(field.Name));
        var typeSyntax = SyntaxFactory.ParseTypeName(GetTypeName(field.Value));
        var semicolonToken = SyntaxFactory.Token(SyntaxKind.SemicolonToken);

        return SyntaxFactory.PropertyDeclaration(typeSyntax, propName)
            .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
            .AddAccessorListAccessors(
                SyntaxFactory.AccessorDeclaration(SyntaxKind.GetAccessorDeclaration).WithSemicolonToken(semicolonToken),
                SyntaxFactory.AccessorDeclaration(SyntaxKind.InitAccessorDeclaration).WithSemicolonToken(semicolonToken)
            );
    }

    private string GetTypeName(CueValueNode node)
    {
        return node switch
        {
            CueStructValue s => _typeNames.GetValueOrDefault(s.Path, "object"),
            CueListValue l => GetListTypeName(l),
            CueSimpleValue sv => MapSimpleKindToCSharpType(sv.Kind),
            _ => "object"
        };
    }

    private string GetListTypeName(CueListValue list)
    {
        var elemType = GetTypeName(list.ElementType);
        return $"List<{elemType}>";
    }

    private static string MapSimpleKindToCSharpType(Kind kind)
    {
        return kind switch
        {
            Kind.Int => "long",
            Kind.Bool => "bool",
            Kind.Float => "double",
            Kind.Number => "double",
            Kind.String => "string",
            Kind.Bytes => "byte[]",
            _ => "object"
        };
    }
}
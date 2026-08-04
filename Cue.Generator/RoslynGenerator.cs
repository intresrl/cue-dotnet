using Cuelang.Cue;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Cue.Generator;

public sealed class RoslynGenerator
{
    private static int _anonymousIndex = 1;
    
    // map from struct path -> generated type name
    private readonly Dictionary<string, string> _typeNames = new();
    
    // map from disjunction path -> (base class name, discriminator field, branch paths)
    private readonly Dictionary<string, (string BaseClassName, string DiscriminatorField, List<string> BranchPaths)> 
        _discriminatedUnions = new();

    public string GenerateCode(CueValueNode root, TextWriter? debugWriter = null)
    {
        // collect struct nodes and assign type names
        CollectStructs(root);
        
        // Debug: Check what we're collecting (if debug output is enabled)
        if (debugWriter != null)
        {
            debugWriter.WriteLine("=== Collected Structs ===");
            foreach (var (path, typeName) in _typeNames)
            {
                debugWriter.WriteLine($"{path} -> {typeName}");
            }
            debugWriter.WriteLine();
        }
        
        // collect discriminated unions
        CollectDiscriminatedUnions(root);
        
        if (debugWriter != null)
        {
            debugWriter.WriteLine("=== Collected Discriminated Unions ===");
            foreach (var (path, (baseClass, discriminator, branches)) in _discriminatedUnions)
            {
                debugWriter.WriteLine($"{path} -> BaseClass: {baseClass}, Discriminator: {discriminator}");
                foreach (var branch in branches)
                {
                    debugWriter.WriteLine($"  - {branch}");
                }
            }
            debugWriter.Flush();
        }
        
        var compilationUnit = SyntaxFactory.CompilationUnit()
            .AddUsings(
                SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System")),
                SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System.Collections.Generic"))
            );

        var members = new List<MemberDeclarationSyntax>();

        // create abstract base classes for discriminated unions first
        foreach (var (_, (baseClassName, _, _)) in _discriminatedUnions.OrderBy(kv => kv.Value.BaseClassName))
        {
            var abstractClass = CreateAbstractBaseClass(baseClassName);
            members.Add(abstractClass);
        }

        // create classes for each struct (keep deterministic order)
        foreach (var (path, typeName) in _typeNames.OrderBy(kv => kv.Value))
        {
            if (!TryFindStruct(root, path, out var structNode)) continue;
            
            // Check if this struct is part of a discriminated union
            var baseClass = FindDiscriminatorBaseClass(path);
            var classDecl = CreateClassDeclaration(typeName, structNode!, baseClass);
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
            case CueDisjunction d:
            {
                foreach (var branch in d.Branches)
                {
                    CollectStructs(branch, visitedStructPaths);
                }
                break;
            }
        }
    }

    private void CollectDiscriminatedUnions(CueValueNode node)
    {
        CollectDiscriminatedUnions(node, new HashSet<string>(StringComparer.Ordinal));
    }

    private void CollectDiscriminatedUnions(CueValueNode node, HashSet<string> visited)
    {
        while (true)
        {
            switch (node)
            {
                case CueDisjunction { IsDiscriminated: true } disjunction when !visited.Add(disjunction.Path):
                    return;
                // Generate base class name from path
                case CueDisjunction { IsDiscriminated: true } disjunction:
                {
                    var baseClassName = GenerateBaseClassName(disjunction.Path);
                    var branchPaths = disjunction.Branches.OfType<CueStructValue>()
                        .Select(b => b.Path)
                        .ToList();

                    _discriminatedUnions[disjunction.Path] = (baseClassName, disjunction.DiscriminatorField!, branchPaths);

                    foreach (var branch in disjunction.Branches)
                    {
                        CollectDiscriminatedUnions(branch, visited);
                    }

                    break;
                }
                case CueStructValue structValue when !visited.Add(structValue.Path):
                    return;
                case CueStructValue structValue:
                {
                    foreach (var field in structValue.Fields)
                    {
                        CollectDiscriminatedUnions(field.Value, visited);
                    }

                    break;
                }
                case CueListValue listValue:
                    node = listValue.ElementType;
                    continue;
                // Regular non-discriminated union - still need to visit branches
                case CueDisjunction regularDisjunction when !visited.Add(regularDisjunction.Path):
                    return;
                case CueDisjunction regularDisjunction:
                {
                    foreach (var branch in regularDisjunction.Branches)
                    {
                        CollectDiscriminatedUnions(branch, visited);
                    }

                    break;
                }
            }

            break;
        }
    }

    private string? FindDiscriminatorBaseClass(string structPath)
    {
        foreach (var (_, (baseClassName, _, branchPaths)) in _discriminatedUnions)
        {
            if (branchPaths.Contains(structPath))
            {
                return baseClassName;
            }
        }
        return null;
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
            case CueDisjunction du:
            {
                foreach (var branch in du.Branches)
                {
                    if (TryFindStruct(branch, path, out result))
                        return true;
                }
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

    private static string GenerateBaseClassName(string path)
    {
        // Generate name like "ValueFormatBase" from a discriminator union path
        var typeName = GenerateTypeName(path);
        return typeName + "Base";
    }

    private static string SanitizeIdentifier(string s)
    {
        if (string.IsNullOrEmpty(s)) return "Anonymous" + _anonymousIndex++;
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

    private static ClassDeclarationSyntax CreateAbstractBaseClass(string className)
    {
        return SyntaxFactory.ClassDeclaration(className)
            .AddModifiers(
                SyntaxFactory.Token(SyntaxKind.PublicKeyword),
                SyntaxFactory.Token(SyntaxKind.AbstractKeyword)
            )
            .WithMembers(SyntaxFactory.List<MemberDeclarationSyntax>());
    }

    private ClassDeclarationSyntax CreateClassDeclaration(string typeName, CueStructValue node, string? baseClass = null)
    {
        var classDecl = SyntaxFactory.ClassDeclaration(typeName)
            .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword));

        // If this class extends a base class, add it
        if (string.IsNullOrEmpty(baseClass))
            return classDecl.AddMembers([
                .. node.Fields.Select(DeclareProperty)
            ]);
        var baseTypeName = SyntaxFactory.ParseTypeName(baseClass);
        var baseTypeList = SyntaxFactory.BaseList(
            SyntaxFactory.SingletonSeparatedList<BaseTypeSyntax>(
                SyntaxFactory.SimpleBaseType(baseTypeName)
            )
        );
        classDecl = classDecl.WithBaseList(baseTypeList);

        return classDecl.AddMembers([
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
            CueDisjunction { IsDiscriminated: true } d => FindDiscriminatorBaseClass(d.Path) ?? "object",
            CueListValue l => GetListTypeName(l),
            CueBoolValue => "bool",
            CueIntValue => "long",
            CueFloatValue => "double",
            CueStringValue => "string",
            CueBytesValue => "byte[]",
            CueNumberValue => "double",
            // TODO: Fix serialization of CueNullValue - currently serialized as object type
            CueNullValue => "object",
            CueBottomValue => throw new InvalidOperationException($"CueBottomValue at {node.Path} cannot be serialized"),
            _ => "object"
        };
    }

    private string GetListTypeName(CueListValue list)
    {
        var elemType = GetTypeName(list.ElementType);
        return $"List<{elemType}>";
    }
}

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Cue.Generator.Roslyn;

public interface IRoslynGenerator
{
    string GenerateCode(CueValueNode root);
}

public sealed class RoslynGenerator(ITypeStore typeStore, IIdentifierNamer namer) : IRoslynGenerator
{
    public string GenerateCode(CueValueNode root)
    {
        typeStore.Collect(root);
        typeStore.CollectionReport();
        
        var compilationUnit = SyntaxFactory.CompilationUnit()
            .AddUsings(
                SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System")),
                SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System.Collections.Generic"))
            );

        // create abstract base classes for discriminated unions first
        var members = typeStore.GetAbstractDefinitions()
            .Select(d => CreateAbstractBaseClass(namer.BaseClassName(d.Disjunction.Path)))
            .Cast<MemberDeclarationSyntax>()
            .ToList();

        // create classes for each struct (keep deterministic order)
        members.AddRange(typeStore.GetConcreteDefinitions()
            .Select(d => CreateClassDeclaration(d.StructNode.Path, d.StructNode, d.BaseClassPath)));

        compilationUnit = compilationUnit.AddMembers([.. members]);
        return compilationUnit.NormalizeWhitespace().ToFullString();
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

    private ClassDeclarationSyntax CreateClassDeclaration(
        string typePath,
        CueStructValue node,
        string? baseClass = null)
    {
        var classDecl = SyntaxFactory.ClassDeclaration(namer.TypeName(typePath))
            .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword));

        // If this class extends a base class, add it
        if (string.IsNullOrEmpty(baseClass))
        {
            return classDecl.AddMembers([
                .. node.Fields.Select(DeclareProperty)
            ]);
        }

        var baseTypeName = SyntaxFactory.ParseTypeName(namer.BaseClassName(baseClass));
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
        var propName = namer.Identifier(field.Name);
        var valueName = typeStore.GetTypeName(field.Value).Format(namer.TypeName, namer.BaseClassName);
        
        var typeSyntax = SyntaxFactory.ParseTypeName(valueName);
        var semicolonToken = SyntaxFactory.Token(SyntaxKind.SemicolonToken);

        return SyntaxFactory.PropertyDeclaration(typeSyntax, propName)
            .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
            .AddAccessorListAccessors(
                SyntaxFactory.AccessorDeclaration(SyntaxKind.GetAccessorDeclaration).WithSemicolonToken(semicolonToken),
                SyntaxFactory.AccessorDeclaration(SyntaxKind.InitAccessorDeclaration).WithSemicolonToken(semicolonToken)
            );
    }
}

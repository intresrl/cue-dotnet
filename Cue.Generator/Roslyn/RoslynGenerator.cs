using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Cue.Generator.Roslyn;

public interface IRoslynGenerator
{
    string GenerateCode(IEnumerable<CueValueNode> root);
}

public sealed class RoslynGenerator(ITypeStore typeStore, IIdentifierNamer namer) : IRoslynGenerator
{
    public string GenerateCode(IEnumerable<CueValueNode> root)
    {
        foreach (var node in root) typeStore.Collect(node);

        var compilationUnit = SyntaxFactory.CompilationUnit()
            .AddUsings(
                SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System")),
                SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System.Collections.Generic"))
            );

        // create abstract base classes for discriminated unions first
        var members = typeStore.GetAbstractDefinitions()
            .Select(d => CreateDisjunction(d))
            .ToList();

        // create classes for each struct (keep deterministic order)
        members.AddRange(typeStore.GetConcreteDefinitions()
            .Select(d => CreateClassDeclaration(d.StructNode.Path, d.StructNode)));

        compilationUnit = compilationUnit.AddMembers([.. members]);
        return compilationUnit.NormalizeWhitespace().ToFullString();
    }

    private MemberDeclarationSyntax CreateDisjunction(DisjunctionDefinition definition)
    {
        var className = namer.BaseClassName(definition.DisjunctionPath);

        var branchRecords = definition.BranchPaths
            .Select(branchPath =>
            {
                var branchTypeName = namer.TypeName(branchPath);
                var recordName = $"As{branchTypeName}";

                return SyntaxFactory.RecordDeclaration(
                        default,
                        SyntaxFactory.TokenList(
                            SyntaxFactory.Token(SyntaxKind.PublicKeyword)),
                        SyntaxFactory.Token(SyntaxKind.RecordKeyword),
                        SyntaxFactory.Identifier(recordName),
                        typeParameterList: null,
                        parameterList: SyntaxFactory.ParameterList(
                            SyntaxFactory.SingletonSeparatedList(
                                SyntaxFactory.Parameter(
                                        SyntaxFactory.Identifier("value"))
                                    .WithType(
                                        SyntaxFactory.IdentifierName(branchTypeName)))),
                        baseList: SyntaxFactory.BaseList(
                            SyntaxFactory.SingletonSeparatedList<BaseTypeSyntax>(
                                SyntaxFactory.SimpleBaseType(
                                    SyntaxFactory.IdentifierName(className)))),
                        constraintClauses: default,
                        members: default)
                    .WithSemicolonToken(
                        SyntaxFactory.Token(SyntaxKind.SemicolonToken));
            })
            .Cast<MemberDeclarationSyntax>()
            .ToList();

        var branchesParameter = SyntaxFactory.Parameter(
                SyntaxFactory.Identifier("Branches"))
            .WithType(
                SyntaxFactory.ArrayType(
                        SyntaxFactory.IdentifierName(className))
                    .AddRankSpecifiers(
                        SyntaxFactory.ArrayRankSpecifier(
                            SyntaxFactory.SingletonSeparatedList<ExpressionSyntax>(
                                SyntaxFactory.OmittedArraySizeExpression()))));

        var validProperty = SyntaxFactory.PropertyDeclaration(
                SyntaxFactory.PredefinedType(
                    SyntaxFactory.Token(SyntaxKind.BoolKeyword)),
                "Valid")
            .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
            .WithExpressionBody(
                SyntaxFactory.ArrowExpressionClause(
                    SyntaxFactory.BinaryExpression(
                        SyntaxKind.EqualsExpression,
                        SyntaxFactory.MemberAccessExpression(
                            SyntaxKind.SimpleMemberAccessExpression,
                            SyntaxFactory.IdentifierName("Branches"),
                            SyntaxFactory.IdentifierName("Length")),
                        SyntaxFactory.LiteralExpression(
                            SyntaxKind.NumericLiteralExpression,
                            SyntaxFactory.Literal(1)))))
            .WithSemicolonToken(
                SyntaxFactory.Token(SyntaxKind.SemicolonToken));

        var valueRecord = SyntaxFactory.RecordDeclaration(
                default,
                SyntaxFactory.TokenList(
                    SyntaxFactory.Token(SyntaxKind.PublicKeyword)),
                SyntaxFactory.Token(SyntaxKind.RecordKeyword),
                SyntaxFactory.Identifier("Value"),
                typeParameterList: null,
                parameterList: SyntaxFactory.ParameterList(
                    SyntaxFactory.SingletonSeparatedList(branchesParameter)),
                baseList: null,
                constraintClauses: default,
                members: SyntaxFactory.SingletonList<MemberDeclarationSyntax>(
                    validProperty))
            .WithSemicolonToken(
                SyntaxFactory.Token(SyntaxKind.SemicolonToken));

        return SyntaxFactory.InterfaceDeclaration(className)
            .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
            .AddMembers([
                ..branchRecords,
                valueRecord
            ]);
    }


    private ClassDeclarationSyntax CreateClassDeclaration(
        string typePath,
        CueStructValue node)
    {
        var classDecl = SyntaxFactory.ClassDeclaration(namer.TypeName(typePath))
            .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword));

        // If this class extends a base class, add it
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
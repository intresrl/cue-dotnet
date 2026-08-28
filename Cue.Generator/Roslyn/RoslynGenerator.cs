using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Reflection;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using static Microsoft.CodeAnalysis.CSharp.SyntaxKind;

namespace Cue.Generator.Roslyn;

public interface IRoslynGenerator
{
    string GenerateCode(IEnumerable<CueValueNode> root);
}

public sealed class RoslynGenerator(ITypeStore typeStore, IIdentifierNamer namer) : IRoslynGenerator
{
    public string GenerateCode(IEnumerable<CueValueNode> root)
    {
        var roots = root.ToArray();
        typeStore.Collect(roots);

        var usings = new List<UsingDirectiveSyntax>();
        var members = new List<MemberDeclarationSyntax>();

        usings.Add(UsingDirective(ParseName("System")));
        usings.Add(UsingDirective(ParseName("System.Collections.Generic")));

        if (roots.Any(ContainsMixedList))
        {
            var prelude = LoadPrelude();
            usings.AddRange(prelude.Usings);
            members.AddRange(prelude.Members);
        }

        // create list defintions
        members.AddRange(typeStore.GetContainerDefinitions()
            .Select(ld => GenerateContainer(namer.TypeName(ld.ListPath, NamingKind.Type), ld.ListType.Format(namer.TypeName))));
        
        // create abstract base classes for discriminated unions first
        members.AddRange(typeStore.GetAbstractDefinitions().Select(CreateDisjunction));

        // create classes for each struct (keep deterministic order)
        members.AddRange(typeStore.GetRecordDefinitions()
            .Select(d => CreateClassDeclaration(d.StructNode.Path, d.StructNode)));
        
        return CompilationUnit()
            .AddUsings(usings.ToArray())
            .AddMembers(members.ToArray())
            .NormalizeWhitespace()
            .ToFullString();
    }

    private static CompilationUnitSyntax LoadPrelude()
    {
        using var stream = Assembly.GetExecutingAssembly()
            .GetManifestResourceStream("Cue.Generator.Prelude.cs")
            ?? throw new InvalidOperationException("Generator prelude resource was not found.");
        using var reader = new StreamReader(stream);
        return CSharpSyntaxTree.ParseText(reader.ReadToEnd()).GetCompilationUnitRoot();
    }

    private static bool ContainsMixedList(CueValueNode node)
    {
        return node.BreadthFirstSearch<bool>(n => n switch
            {
                CueListValue { Tail: not null, Indexed.Count: > 0 } => ([], [true]),
                CueListValue { Tail: null, Indexed: var ind } => ([.. ind], []),
                CueListValue { Tail: { } tail, Indexed: var ind } => ([.. ind, tail], []),
                CueStructValue { Fields: var fs } => (fs.Select(f => f.Value), []),
                CueDisjunction { Branches: var bs } => (bs, []),
                CueNullable { Value: var v } => ([v], []),
                _ => ([], [false])
            })
            .Any(e => e);
    }

    private MemberDeclarationSyntax CreateDisjunction(DisjunctionDefinition definition)
    {
        var className = namer.TypeName(definition.DisjunctionPath, NamingKind.Disjunction);

        var branchRecords = definition.BranchPaths
            .Select(MemberDeclarationSyntax (branchPath) =>
            {
                var branchTypeName = namer.TypeName(branchPath, NamingKind.DisjunctionBranch);
                
                // TODO: this generator is buggy as it assumes all branches are references. Rewrite TypeStore so it resolves
                //  non reference branches, a.k.a. anonymous definitions, with a base name and a reference to that base name 
                //  is inferred here
                var typeName = namer.TypeName(branchPath, NamingKind.Type);

                return RecordDeclaration(
                        attributeLists: [],
                        modifiers: TokenList(Token(PublicKeyword)), 
                        keyword: Token(RecordKeyword),
                        identifier: Identifier(branchTypeName),
                        typeParameterList: null,
                        parameterList: ParameterList(
                            SingletonSeparatedList(
                                Parameter(Identifier("value"))
                                    .WithType(IdentifierName(typeName)))),
                        baseList: BaseList(
                            SingletonSeparatedList<BaseTypeSyntax>(
                                SimpleBaseType(IdentifierName(className)))),
                        constraintClauses: [],
                        members: [])
                    .WithSemicolonToken(Token(SemicolonToken));
            });

        var branchesParameter = Parameter(Identifier("Branches"))
            .WithType(
                ArrayType(IdentifierName(className))
                    .AddRankSpecifiers(
                        ArrayRankSpecifier(
                            SingletonSeparatedList<ExpressionSyntax>(
                                OmittedArraySizeExpression()))));

        var validProperty = PropertyDeclaration(PredefinedType(Token(BoolKeyword)), "Valid")
            .AddModifiers(Token(PublicKeyword))
            .WithExpressionBody(
                ArrowExpressionClause(
                    BinaryExpression(
                        EqualsExpression,
                        MemberAccessExpression(
                            SimpleMemberAccessExpression,
                            IdentifierName("Branches"),
                            IdentifierName("Length")),
                        LiteralExpression(
                            NumericLiteralExpression,
                            Literal(1)))))
            .WithSemicolonToken(
                Token(SemicolonToken));

        var valueRecord = RecordDeclaration(
                attributeLists: [],
                modifiers: TokenList(Token(PublicKeyword)), Token(RecordKeyword),
                identifier: Identifier("Value"),
                typeParameterList: null,
                parameterList: ParameterList(SingletonSeparatedList(branchesParameter)),
                baseList: null,
                constraintClauses: [],
                members: SingletonList<MemberDeclarationSyntax>(validProperty))
            .WithSemicolonToken(Token(SemicolonToken));

        return InterfaceDeclaration(className)
            .AddModifiers(Token(PublicKeyword))
            .AddMembers([..branchRecords, valueRecord]);
    }


    private ClassDeclarationSyntax CreateClassDeclaration(string typePath, CueStructValue node)
    {
        var classDecl = ClassDeclaration(namer.TypeName(typePath, NamingKind.Type))
            .AddModifiers(Token(PublicKeyword));

        // If this class extends a base class, add it
        return classDecl.AddMembers([
            .. node.Fields.Select(DeclareProperty)
        ]);
    }

    private PropertyDeclarationSyntax DeclareProperty(CueStructField field)
    {
        var propName = namer.Identifier(field.Name);
        var valueName = typeStore.GetTypeName(field.Value).Format(namer.TypeName);

        // TODO: solve abstraction inversion on type name for complex types (like List<...>)
        return PropertyDeclaration(ParseTypeName(valueName), propName)
            .AddModifiers(Token(PublicKeyword))
            .AddAccessorListAccessors(
                AccessorDeclaration(GetAccessorDeclaration).WithSemicolonToken(Token(SemicolonToken)),
                AccessorDeclaration(InitAccessorDeclaration).WithSemicolonToken(Token(SemicolonToken))
            );
    }

    public static StructDeclarationSyntax GenerateContainer(string containerName, string typeName)
    {
        var typeSyntax = ParseTypeName(typeName);

        return StructDeclaration(containerName)
            .AddModifiers(Token(PublicKeyword), Token(ReadOnlyKeyword))
            .WithParameterList(
                ParameterList(
                    SingletonSeparatedList(
                        Parameter(Identifier("value"))
                            .WithType(typeSyntax))))
            .AddMembers(
                PropertyDeclaration(typeSyntax, "Value")
                    .AddModifiers(Token(PublicKeyword))
                    .WithAccessorList(
                        AccessorList(
                            SingletonList(
                                AccessorDeclaration(GetAccessorDeclaration)
                                    .WithSemicolonToken(
                                        Token(SemicolonToken)))))
                    .WithInitializer(EqualsValueClause(IdentifierName("value")))
                    .WithSemicolonToken(Token(SemicolonToken)),
                ConversionOperatorDeclaration(
                        Token(ImplicitKeyword),
                        IdentifierName(containerName))
                    .AddModifiers(Token(PublicKeyword))
                    .WithParameterList(
                        ParameterList(
                            SingletonSeparatedList(
                                Parameter(Identifier("value"))
                                    .WithType(typeSyntax))))
                    .WithExpressionBody(
                        ArrowExpressionClause(
                            ImplicitObjectCreationExpression(
                                ArgumentList(
                                    SingletonSeparatedList(
                                        Argument(
                                            IdentifierName("value")))), 
                                null)))
                    .WithSemicolonToken(Token(SemicolonToken)))
            .NormalizeWhitespace();
    }
}
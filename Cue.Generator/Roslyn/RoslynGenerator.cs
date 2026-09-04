using System.Reflection;
using Cuelang.Cue;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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

        var usings = new List<UsingDirectiveSyntax>
        {
            UsingDirective(ParseName("System")),
            UsingDirective(ParseName("System.Collections.Generic")),
            UsingDirective(ParseName("System.Numerics"))
        };
        var members = new List<MemberDeclarationSyntax>();

        if (roots.Any(ContainsMixedList))
        {
            var prelude = LoadPrelude();
            usings.AddRange(prelude.Usings);
            members.AddRange(prelude.Members);
        }

        members.AddRange(typeStore.GetContainerDefinitions()
            .Select(definition => GenerateContainer(
                namer.TypeName(definition.ListPath, NamingKind.Type),
                definition.ListType.Format(namer.TypeName))));
        members.AddRange(typeStore.GetAbstractDefinitions().Select(CreateDisjunction));
        members.AddRange(typeStore.GetConcreteDefinitions()
            .Select(CreateTypeDefinition)
            .Where(member => member is not null)
            .Cast<MemberDeclarationSyntax>());

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
            CueListValue { Tail: null, Indexed: var indexed } => ([.. indexed], []),
            CueListValue { Tail: { } tail, Indexed: var indexed } => ([.. indexed, tail], []),
            CueStructValue { Fields: var fields } => (fields.Select(f => f.Value), []),
            CueDisjunction { Branches: var branches } => (branches, []),
            CueNullable { Value: var value } => ([value], []),
            _ => ([], [false])
        }).Any(e => e);
    }

    private MemberDeclarationSyntax? CreateTypeDefinition(ConcreteDefinition definition)
    {
        if (definition.ValueNode is CueIntValue or CueFloatValue or CueStringValue or CueBoolValue or CueNumberValue
            && typeStore.GetConstraint(definition.ValueNode.Path) is null)
        {
            return null;
        }

        return definition.ValueNode switch
        {
            CueIntValue value => CreatePrimitiveTypeDefinition(value.Path, value.Constraint),
            CueFloatValue value => CreatePrimitiveTypeDefinition(value.Path, value.Constraint),
            CueStringValue value => CreatePrimitiveTypeDefinition(value.Path, value.Constraint),
            CueBoolValue value => CreatePrimitiveTypeDefinition(value.Path, value.Constraint),
            CueNumberValue value => CreatePrimitiveTypeDefinition(value.Path, typeStore.GetConstraint(value.Path)),
            CueStructValue value => CreateClassDeclaration(value.Path, value),
            _ => CreateClassDeclaration(definition.ValueNode.Path, null)
        };
    }

    private StructDeclarationSyntax CreatePrimitiveTypeDefinition(string typePath, CueExpr? constraint)
    {
        var valueType = GetValueTypeForPath(typePath);
        return StructDeclaration(namer.TypeName(typePath, NamingKind.Type))
            .AddModifiers(Token(PublicKeyword), Token(ReadOnlyKeyword), Token(RecordKeyword))
            .WithParameterList(ParameterList(SingletonSeparatedList(
                Parameter(Identifier("Value")).WithType(ParseTypeName(valueType)))))
            .AddMembers(CreateIsValidMethod(valueType, constraint));
    }

    private string GetValueTypeForPath(string typePath) => typeStore.GetValueType(typePath) switch
    {
        "Byte" => "byte",
        "SByte" => "sbyte",
        "UInt16" => "ushort",
        "Int16" => "short",
        "UInt32" => "uint",
        "Int32" => "int",
        "UInt64" => "ulong",
        "Int64" => "long",
        "Single" => "float",
        "Double" => "double",
        null => "object",
        var valueType => valueType
    };

    private static MethodDeclarationSyntax CreateIsValidMethod(string valueType, CueExpr? constraint)
    {
        var kind = valueType switch
        {
            "byte" or "sbyte" or "ushort" or "short" or "uint" or "int" or "ulong" or "long" or "BigInteger" => Kind.Int,
            "float" or "double" => Kind.Float,
            "string" => Kind.String,
            "bool" => Kind.Bool,
            _ => Kind.Top
        };
        var expression = ConstraintCodeGenerator.GenerateValidationExpression(constraint, "value", kind);
        return MethodDeclaration(PredefinedType(Token(BoolKeyword)), "IsValid")
            .AddModifiers(Token(PublicKeyword), Token(StaticKeyword))
            .AddParameterListParameters(Parameter(Identifier("value")).WithType(ParseTypeName(valueType)))
            .WithExpressionBody(ArrowExpressionClause(expression))
            .WithSemicolonToken(Token(SemicolonToken));
    }

    private MemberDeclarationSyntax CreateDisjunction(DisjunctionDefinition definition)
    {
        var className = namer.TypeName(definition.DisjunctionPath, NamingKind.Disjunction);
        var branchRecords = definition.BranchPaths.Select(branchPath =>
            (MemberDeclarationSyntax)RecordDeclaration(
                default,
                TokenList(Token(PublicKeyword)),
                Token(RecordKeyword),
                Identifier(namer.TypeName(branchPath, NamingKind.DisjunctionBranch)),
                null,
                ParameterList(SingletonSeparatedList(
                    Parameter(Identifier("value")).WithType(IdentifierName(
                        namer.TypeName(branchPath, NamingKind.Type))))),
                BaseList(SingletonSeparatedList<BaseTypeSyntax>(
                    SimpleBaseType(IdentifierName(className)))),
                default,
                default)
            .WithSemicolonToken(Token(SemicolonToken)));

        var branchesParameter = Parameter(Identifier("Branches")).WithType(
            ArrayType(IdentifierName(className)).AddRankSpecifiers(
                ArrayRankSpecifier(SingletonSeparatedList<ExpressionSyntax>(
                    OmittedArraySizeExpression()))));
        var validProperty = PropertyDeclaration(PredefinedType(Token(BoolKeyword)), "Valid")
            .AddModifiers(Token(PublicKeyword))
            .WithExpressionBody(ArrowExpressionClause(
                BinaryExpression(EqualsExpression,
                    MemberAccessExpression(SimpleMemberAccessExpression, IdentifierName("Branches"), IdentifierName("Length")),
                    LiteralExpression(NumericLiteralExpression, Literal(1)))))
            .WithSemicolonToken(Token(SemicolonToken));
        var valueRecord = RecordDeclaration(
            default,
            TokenList(Token(PublicKeyword)),
            Token(RecordKeyword),
            Identifier("Value"),
            null,
            ParameterList(SingletonSeparatedList(branchesParameter)),
            null,
            default,
            SingletonList<MemberDeclarationSyntax>(validProperty))
            .WithSemicolonToken(Token(SemicolonToken));

        return InterfaceDeclaration(className)
            .AddModifiers(Token(PublicKeyword))
            .AddMembers([.. branchRecords, valueRecord]);
    }

    private ClassDeclarationSyntax CreateClassDeclaration(string typePath, CueStructValue? node)
    {
        var declaration = ClassDeclaration(namer.TypeName(typePath, NamingKind.Type))
            .AddModifiers(Token(PublicKeyword));
        return node is null ? declaration : declaration.AddMembers([.. node.Fields.Select(DeclareProperty)]);
    }

    private PropertyDeclarationSyntax DeclareProperty(CueStructField field)
    {
        return PropertyDeclaration(ParseTypeName(typeStore.GetTypeName(field.Value).Format(namer.TypeName)),
                namer.Identifier(field.Name))
            .AddModifiers(Token(PublicKeyword))
            .AddAccessorListAccessors(
                AccessorDeclaration(GetAccessorDeclaration).WithSemicolonToken(Token(SemicolonToken)),
                AccessorDeclaration(InitAccessorDeclaration).WithSemicolonToken(Token(SemicolonToken)));
    }

    public static StructDeclarationSyntax GenerateContainer(string containerName, string typeName)
    {
        var typeSyntax = ParseTypeName(typeName);
        return StructDeclaration(containerName)
            .AddModifiers(Token(PublicKeyword), Token(ReadOnlyKeyword))
            .WithParameterList(ParameterList(SingletonSeparatedList(
                Parameter(Identifier("value")).WithType(typeSyntax))))
            .AddMembers(
                PropertyDeclaration(typeSyntax, "Value")
                    .AddModifiers(Token(PublicKeyword))
                    .WithAccessorList(AccessorList(SingletonList(
                        AccessorDeclaration(GetAccessorDeclaration).WithSemicolonToken(Token(SemicolonToken)))))
                    .WithInitializer(EqualsValueClause(IdentifierName("value")))
                    .WithSemicolonToken(Token(SemicolonToken)),
                ConversionOperatorDeclaration(Token(ImplicitKeyword), IdentifierName(containerName))
                    .AddModifiers(Token(PublicKeyword))
                    .WithParameterList(ParameterList(SingletonSeparatedList(
                        Parameter(Identifier("value")).WithType(typeSyntax))))
                    .WithExpressionBody(ArrowExpressionClause(
                        ImplicitObjectCreationExpression(ArgumentList(SingletonSeparatedList(
                            Argument(IdentifierName("value")))), null)))
                    .WithSemicolonToken(Token(SemicolonToken)))
            .NormalizeWhitespace();
    }
}

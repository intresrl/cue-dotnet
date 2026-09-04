using Cuelang.Cue;
using Microsoft.CodeAnalysis;
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
        typeStore.Collect(root);

        var compilationUnit = CompilationUnit()
            .AddUsings(
                UsingDirective(ParseName("System.Numerics"))
            );

        // create abstract base classes for discriminated unions first
        var members = typeStore.GetAbstractDefinitions()
            .Select(CreateDisjunction)
            .ToList();

        // create classes for each struct (keep deterministic order)
        members.AddRange(typeStore.GetConcreteDefinitions()
            .Select(CreateTypeDefinition)
            .Where(m => m != null)
            .Cast<MemberDeclarationSyntax>()); // Filter out nulls from skipped bare types

        compilationUnit = compilationUnit.AddMembers([.. members]);
        return compilationUnit.NormalizeWhitespace().ToFullString();
    }

    private MemberDeclarationSyntax? CreateTypeDefinition(ConcreteDefinition definition)
    {
        // Skip bare types without constraints
        if (definition.ValueNode is CueIntValue or CueFloatValue or CueStringValue or CueBoolValue or CueNumberValue
            && typeStore.GetConstraint(definition.ValueNode.Path) is null or CueAnyExpr)
            return null;

        return definition.ValueNode switch
        {
            CueIntValue intVal => CreatePrimitiveTypeDefinition(definition.ValueNode.Path, intVal.Constraint),
            CueFloatValue floatVal => CreatePrimitiveTypeDefinition(definition.ValueNode.Path, floatVal.Constraint),
            CueStringValue stringVal => CreatePrimitiveTypeDefinition(definition.ValueNode.Path, stringVal.Constraint),
            CueBoolValue boolVal => CreatePrimitiveTypeDefinition(definition.ValueNode.Path, boolVal.Constraint),
            CueNumberValue => CreatePrimitiveTypeDefinition(definition.ValueNode.Path,
                typeStore.GetConstraint(definition.ValueNode.Path)),
            CueStructValue structVal => CreateClassDeclaration(structVal.Path, structVal),
            _ => CreateClassDeclaration(definition.ValueNode.Path, null)
        };
    }

    private StructDeclarationSyntax CreatePrimitiveTypeDefinition(string typePath, CueExpr? constraint)
    {
        var typeName = namer.TypeName(typePath);
        var valueType = GetValueTypeForPath(typePath);

        var valueParameter = Parameter(Identifier("Value"))
            .WithType(ParseTypeName(valueType));

        var structDecl = StructDeclaration(typeName)
            .AddModifiers(Token(PublicKeyword), Token(ReadOnlyKeyword), Token(RecordKeyword))
            .WithParameterList(ParameterList(SingletonSeparatedList(valueParameter)));

        // Generate IsValid static method
        var isValidMethod = CreateIsValidMethod(valueType, constraint);
        structDecl = structDecl.AddMembers(isValidMethod);

        return structDecl;
    }

    // Use lowercase names for standard types
    private string GetValueTypeForPath(string typePath)
    {
        return typeStore.GetValueType(typePath) switch
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
            var e => e
        };
    }

    private MethodDeclarationSyntax CreateIsValidMethod(string valueType, CueExpr? constraint)
    {
        var kind = GetKindFromValueType(valueType);
        var validationExpression = ConstraintCodeGenerator.GenerateValidationExpression(constraint, "value", kind);

        var method = MethodDeclaration(PredefinedType(Token(BoolKeyword)), "IsValid")
            .AddModifiers(Token(PublicKeyword), Token(StaticKeyword))
            .AddParameterListParameters(Parameter(Identifier("value")).WithType(ParseTypeName(valueType)))
            .WithExpressionBody(ArrowExpressionClause(validationExpression))
            .WithSemicolonToken(Token(SemicolonToken));

        return method;
    }

    private static Kind GetKindFromValueType(string valueType)
    {
        return valueType switch
        {
            "byte" or "sbyte" or "ushort" or "short" or "uint" or "int" or "ulong" or "long"
                or "BigInteger" => Kind.Int,
            "float" or "double" => Kind.Float,
            "string" => Kind.String,
            "bool" => Kind.Bool,
            _ => Kind.Top
        };
    }

    private MemberDeclarationSyntax CreateDisjunction(DisjunctionDefinition definition)
    {
        var className = namer.DisjunctionName(definition.DisjunctionPath);

        var branchRecords = definition.BranchPaths
            .Select(branchPath =>
            {
                // TODO: this generator is buggy as it assumes all branches are references. Rewrite TypeStore so it resolves
                //  non reference branches, a.k.a. anonymous definitions, with a base name and a reference to that base name 
                //  is inferred here
                var branchTypeName = namer.TypeName(branchPath);
                var recordName = $"As{branchTypeName}";

                return RecordDeclaration(
                        default,
                        TokenList(Token(PublicKeyword)),
                        Token(RecordKeyword),
                        Identifier(recordName),
                        null,
                        ParameterList(
                            SingletonSeparatedList(
                                Parameter(Identifier("value"))
                                    .WithType(IdentifierName(branchTypeName)))),
                        BaseList(
                            SingletonSeparatedList<BaseTypeSyntax>(
                                SimpleBaseType(IdentifierName(className)))),
                        default,
                        default)
                    .WithSemicolonToken(Token(SemicolonToken));
            })
            .Cast<MemberDeclarationSyntax>()
            .ToList();

        var branchesParameter = Parameter(
                Identifier("Branches"))
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
                        LiteralExpression(NumericLiteralExpression, Literal(1)))))
            .WithSemicolonToken(
                Token(SemicolonToken));

        var valueRecord = RecordDeclaration(
                default,
                TokenList(Token(PublicKeyword)),
                Token(RecordKeyword),
                Identifier("Value"),
                null,
                ParameterList(
                    SingletonSeparatedList(branchesParameter)),
                null,
                default,
                SingletonList<MemberDeclarationSyntax>(validProperty))
            .WithSemicolonToken(
                Token(SemicolonToken));

        return InterfaceDeclaration(className)
            .AddModifiers(Token(PublicKeyword))
            .AddMembers([
                .. branchRecords,
                valueRecord
            ]);
    }

    private ClassDeclarationSyntax CreateClassDeclaration(string typePath, CueStructValue? node)
    {
        var classDecl = ClassDeclaration(namer.TypeName(typePath))
            .AddModifiers(Token(PublicKeyword));

        // If this class extends a base class, add it
        if (node != null)
            return classDecl.AddMembers([
                .. node.Fields.Select(DeclareProperty)
            ]);

        return classDecl;
    }

    private PropertyDeclarationSyntax DeclareProperty(CueStructField field)
    {
        var propName = namer.Identifier(field.Name);
        var valueName = typeStore.GetTypeName(field.Value).Format(namer.TypeName, namer.DisjunctionName);

        var typeSyntax = ParseTypeName(valueName);
        var semicolonToken = Token(SemicolonToken);

        return PropertyDeclaration(typeSyntax, propName)
            .AddModifiers(Token(PublicKeyword))
            .AddAccessorListAccessors(
                AccessorDeclaration(GetAccessorDeclaration).WithSemicolonToken(semicolonToken),
                AccessorDeclaration(InitAccessorDeclaration).WithSemicolonToken(semicolonToken)
            );
    }
}
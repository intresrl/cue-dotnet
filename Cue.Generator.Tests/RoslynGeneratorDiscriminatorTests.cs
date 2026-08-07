namespace Cue.Generator.Tests;

public sealed class RoslynGeneratorDiscriminatorTests
{
    [Fact]
    public void GeneratesAbstractBaseClassForDiscriminatedUnion()
    {
        using var ctx = new CueContext();
        using var value = ctx.Compile("""
            #TypeA: {type: "a", data: string}
            #TypeB: {type: "b", data: int}
            
            config: {
                format: #TypeA | #TypeB
            }
            """);

        var node = value.ToCueValueNode();
        var gen = new RoslynGenerator();
        var code = gen.GenerateCode(node);

        // Should contain abstract base class
        Assert.Contains("public abstract class", code);
        Assert.Contains("FormatBase", code);
    }

    [Fact]
    public void GeneratesConcreteClassesExtendingAbstractBase()
    {
        using var ctx = new CueContext();
        using var value = ctx.Compile("""
            #OptionA: {type: "a", valueA: string}
            #OptionB: {type: "b", valueB: int}
            
            item: #OptionA | #OptionB
            """);

        var node = value.ToCueValueNode();
        var gen = new RoslynGenerator();
        var code = gen.GenerateCode(node);

        // Should contain concrete classes
        Assert.Contains("public class OptionA : ", code);
        Assert.Contains("public class OptionB : ", code);
    }

    [Fact]
    public void GeneratesDiscriminatorFieldInConcreteClasses()
    {
        using var ctx = new CueContext();
        using var value = ctx.Compile("""
            #StatusActive: {type: "active", duration: int}
            #StatusInactive: {type: "inactive", reason: string}
            
            status: #StatusActive | #StatusInactive
            """);

        var node = value.ToCueValueNode();
        var gen = new RoslynGenerator();
        var code = gen.GenerateCode(node);

        // Should include the type field in generated classes
        Assert.Contains("public string Type", code);
    }

    [Fact]
    public void GeneratesCorrectPropertyNamesForFields()
    {
        using var ctx = new CueContext();
        using var value = ctx.Compile("""
            #FormatA: {type: "date", datePattern: string}
            #FormatB: {type: "time", timeZone: string}
            
            format: #FormatA | #FormatB
            """);

        var node = value.ToCueValueNode();
        var gen = new RoslynGenerator();
        var code = gen.GenerateCode(node);

        // Should convert field names to PascalCase
        Assert.Contains("public string DatePattern", code);
        Assert.Contains("public string TimeZone", code);
    }

    [Fact]
    public void HandlesMultipleBranchesInDiscriminatedUnion()
    {
        using var ctx = new CueContext();
        using var value = ctx.Compile("""
            #TypeA: {type: "a", fieldA: string}
            #TypeB: {type: "b", fieldB: int}
            #TypeC: {type: "c", fieldC: bool}
            
            choice: #TypeA | #TypeB | #TypeC
            """);

        var node = value.ToCueValueNode();
        var gen = new RoslynGenerator();
        var code = gen.GenerateCode(node);

        // Should generate all three concrete classes
        Assert.Contains("public class TypeA", code);
        Assert.Contains("public class TypeB", code);
        Assert.Contains("public class TypeC", code);
    }

    [Fact]
    public void GeneratesAbstractBaseClassForEachDiscriminatedUnion()
    {
        using var ctx = new CueContext();
        using var value = ctx.Compile("""
            #FormatA: {type: "a"}
            #FormatB: {type: "b"}
            
            #StatusX: {kind: "x"}
            #StatusY: {kind: "y"}
            
            config: {
                format: #FormatA | #FormatB
                status: #StatusX | #StatusY
            }
            """);

        var node = value.ToCueValueNode();
        var gen = new RoslynGenerator();
        var code = gen.GenerateCode(node);

        // Should generate multiple abstract base classes
        Assert.Contains("public abstract class FormatBase", code);
        Assert.Contains("public abstract class StatusBase", code);
    }

    [Fact]
    public void GeneratesCorrectTypeMappings()
    {
        using var ctx = new CueContext();
        using var value = ctx.Compile("""
            #NumberOption: {type: "number", value: 123}
            #StringOption: {type: "string", value: "text"}
            
            data: #NumberOption | #StringOption
            """);

        var node = value.ToCueValueNode();
        var gen = new RoslynGenerator();
        var code = gen.GenerateCode(node);

        // Should map Cue types to C# types correctly
        Assert.Contains("public long Value", code); // int -> long
        Assert.Contains("public string Value", code); // string -> string
    }

    [Fact]
    public void HandlesNestedStructsInDiscriminatedUnion()
    {
        using var ctx = new CueContext();
        using var value = ctx.Compile("""
            #ComplexA: {
                type: "a"
                nested: {
                    inner: string
                }
            }
            #ComplexB: {
                type: "b"
                nested: {
                    inner: int
                }
            }
            
            data: #ComplexA | #ComplexB
            """);

        var node = value.ToCueValueNode();
        var gen = new RoslynGenerator();
        var code = gen.GenerateCode(node);

        // Should generate nested class types
        Assert.Contains("public class", code);
    }

    [Fact]
    public void GeneratesListPropertiesCorrectly()
    {
        using var ctx = new CueContext();
        using var value = ctx.Compile("""
            #ItemA: {type: "a", tags: [...string]}
            #ItemB: {type: "b", scores: [...int]}
            
            item: #ItemA | #ItemB
            """);

        var node = value.ToCueValueNode();
        var gen = new RoslynGenerator();
        var code = gen.GenerateCode(node);

        // Should generate List properties
        Assert.Contains("List<string>", code);
        Assert.Contains("List<long>", code);
    }

    [Fact]
    public void ProducesValidCSharpSyntax()
    {
        using var ctx = new CueContext();
        using var value = ctx.Compile("""
            #OptionA: {type: "a", name: string}
            #OptionB: {type: "b", count: int}
            
            config: {
                choice: #OptionA | #OptionB
            }
            """);

        var node = value.ToCueValueNode();
        var gen = new RoslynGenerator();
        var code = gen.GenerateCode(node);

        // Should be valid C# (basic check)
        Assert.Contains("public abstract class", code);
        Assert.Contains("{", code);
        Assert.Contains("}", code);
        Assert.Contains("public class", code);
        Assert.Contains("public string", code);
        Assert.Contains("public long", code);
    }

    [Fact]
    public void GeneratesPropertiesWithGetInit()
    {
        using var ctx = new CueContext();
        using var value = ctx.Compile("""
            #Option1: {type: "1", data: string}
            #Option2: {type: "2", data: int}
            
            value: #Option1 | #Option2
            """);

        var node = value.ToCueValueNode();
        var gen = new RoslynGenerator();
        var code = gen.GenerateCode(node);

        // Should use get; init; pattern
        Assert.Contains("get;", code);
        Assert.Contains("init;", code);
    }

    [Fact]
    public void GeneratesAllBranchesWhenDiscriminatorNotDetected()
    {
        // Even if discriminator is not detected, should still generate all structs
        using var ctx = new CueContext();
        using var value = ctx.Compile("""
            #StructA: {fieldA: string}
            #StructB: {fieldB: int}
            
            choice: #StructA | #StructB
            """);

        var node = value.ToCueValueNode();
        var gen = new RoslynGenerator();
        var code = gen.GenerateCode(node);

        // Should still generate the classes
        Assert.Contains("public class StructA", code);
        Assert.Contains("public class StructB", code);
    }

    [Fact]
    public void RealWorldAnnotationElementGeneration()
    {
        using var ctx = new CueContext();
        using var value = ctx.Compile("""
            #DateTimeDefinition: {
                type: "datetime"
                format: string
            }
            #InitialsDefinition: {
                type: "initials"
                maxInitials: int
            }
            #TextDefinition: {
                type: "text"
                maxLength?: int
            }
            
            #AnnotationElement: {
                position: {x: int, y: int}
                size: {width: int, height: int}
                valueFormat: #DateTimeDefinition | #InitialsDefinition | #TextDefinition
            }
            """);

        var node = value.ToCueValueNode();
        var gen = new RoslynGenerator();
        var code = gen.GenerateCode(node);

        // Should generate abstract base class and concrete implementations
        Assert.Contains("public abstract class ValueFormatBase", code);
        Assert.Contains("public class DateTimeDefinition : ValueFormatBase", code);
        Assert.Contains("public class InitialsDefinition : ValueFormatBase", code);
        Assert.Contains("public class TextDefinition : ValueFormatBase", code);
        
        // Should include all properties
        Assert.Contains("public string Format", code);
        Assert.Contains("public long MaxInitials", code);
        Assert.Contains("public long MaxLength", code);
    }

    [Fact]
    public void GeneratesCorrectPublicModifiers()
    {
        using var ctx = new CueContext();
        using var value = ctx.Compile("""
            #TypeA: {type: "a", value: string}
            #TypeB: {type: "b", value: int}
            
            item: #TypeA | #TypeB
            """);

        var node = value.ToCueValueNode();
        var gen = new RoslynGenerator();
        var code = gen.GenerateCode(node);

        // All classes and properties should be public
        Assert.Contains("public abstract class", code);
        Assert.Contains("public class", code);
        Assert.Contains("public string", code);
        Assert.Contains("public long", code);
    }
}

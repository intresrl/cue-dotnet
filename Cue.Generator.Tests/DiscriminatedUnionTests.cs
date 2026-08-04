namespace Cue.Generator.Tests;

public sealed class DiscriminatedUnionTests
{
    [Fact]
    public void DetectsSimpleDiscriminatorUnion()
    {
        using var ctx = new CueContext();
        using var value = ctx.Compile("""
            #DateTimeFormat: {type: "datetime", format: string}
            #TextFormat: {type: "text", maxLength: int}
            
            valueFormat: #DateTimeFormat | #TextFormat
            """);

        using var valueFormatField = value.Lookup("valueFormat");
        var node = valueFormatField.ToCueValueNode();

        Assert.NotNull(node);
        Assert.IsType<CueDisjunction>(node);
        var discriminator = (CueDisjunction)node;
        
        Assert.True(discriminator.IsDiscriminated);
        Assert.Equal("type", discriminator.DiscriminatorField);
        Assert.Equal(2, discriminator.Branches.Count);
    }

    [Fact]
    public void DiscriminatedUnionBranchesAreStructs()
    {
        using var ctx = new CueContext();
        using var value = ctx.Compile("""
            #Option1: {type: "opt1", value: string}
            #Option2: {type: "opt2", count: int}
            
            choice: #Option1 | #Option2
            """);

        using var choiceField = value.Lookup("choice");
        var node = choiceField.ToCueValueNode();

        Assert.IsType<CueDisjunction>(node);
        var discriminator = (CueDisjunction)node;
        
        Assert.All(discriminator.Branches, branch => Assert.IsType<CueStructValue>(branch));
    }

    [Fact]
    public void DiscriminatorUnionWithThreeBranches()
    {
        using var ctx = new CueContext();
        using var value = ctx.Compile("""
            #TypeA: {type: "a", fieldA: string}
            #TypeB: {type: "b", fieldB: int}
            #TypeC: {type: "c", fieldC: bool}
            
            item: #TypeA | #TypeB | #TypeC
            """);

        using var itemField = value.Lookup("item");
        var node = itemField.ToCueValueNode();

        Assert.IsType<CueDisjunction>(node);
        var discriminator = (CueDisjunction)node;
        
        Assert.True(discriminator.IsDiscriminated);
        Assert.Equal(3, discriminator.Branches.Count);
    }

    [Fact]
    public void DiscriminatorUnionDetectsKindField()
    {
        using var ctx = new CueContext();
        using var value = ctx.Compile("""
            #TypeA: {kind: "a", data: string}
            #TypeB: {kind: "b", data: int}
            
            message: #TypeA | #TypeB
            """);

        using var messageField = value.Lookup("message");
        var node = messageField.ToCueValueNode();

        Assert.IsType<CueDisjunction>(node);
        var discriminator = (CueDisjunction)node;
        
        Assert.True(discriminator.IsDiscriminated);
        Assert.Equal("kind", discriminator.DiscriminatorField);
    }

    [Fact]
    public void DiscriminatorUnionDetectsVariantField()
    {
        using var ctx = new CueContext();
        using var value = ctx.Compile("""
            #VarA: {variant: "a", value: string}
            #VarB: {variant: "b", value: int}
            
            result: #VarA | #VarB
            """);

        using var resultField = value.Lookup("result");
        var node = resultField.ToCueValueNode();

        Assert.IsType<CueDisjunction>(node);
        var discriminator = (CueDisjunction)node;
        
        Assert.True(discriminator.IsDiscriminated);
        Assert.Equal("variant", discriminator.DiscriminatorField);
    }

    [Fact]
    public void NonDiscriminatedUnionStaysAsDisjunction()
    {
        using var ctx = new CueContext();
        using var value = ctx.Compile("value: int | string | bool");

        using var valueField = value.Lookup("value");
        var node = valueField.ToCueValueNode();

        // Should be a disjunction without discriminator
        Assert.IsType<CueDisjunction>(node);
        var disjunction = (CueDisjunction)node;
        Assert.False(disjunction.IsDiscriminated);
        Assert.Null(disjunction.DiscriminatorField);
    }

    [Fact]
    public void StructsWithoutCommonDiscriminatorStayAsDisjunction()
    {
        using var ctx = new CueContext();
        using var value = ctx.Compile("""
            #StructA: {name: string}
            #StructB: {count: int}
            
            value: #StructA | #StructB
            """);

        using var valueField = value.Lookup("value");
        var node = valueField.ToCueValueNode();

        // Should be a disjunction without discriminator
        Assert.IsType<CueDisjunction>(node);
        var disjunction = (CueDisjunction)node;
        Assert.False(disjunction.IsDiscriminated);
    }

    [Fact]
    public void DiscriminatorUnionPreservesPath()
    {
        using var ctx = new CueContext();
        using var value = ctx.Compile("""
            #TypeA: {type: "a", data: string}
            #TypeB: {type: "b", data: int}
            
            config: {
                item: #TypeA | #TypeB
            }
            """);

        using var config = value.Lookup("config");
        using var itemField = config.Lookup("item");
        var node = itemField.ToCueValueNode();

        Assert.IsType<CueDisjunction>(node);
        var discriminator = (CueDisjunction)node;
        
        Assert.Equal("config.item", discriminator.Path);
    }

    [Fact]
    public void DiscriminatorUnionWithMoreStructFields()
    {
        using var ctx = new CueContext();
        using var value = ctx.Compile("""
            #Status1: {
                type: "active"
                activeTime: string
                description: string
            }
            #Status2: {
                type: "inactive"
                inactiveReason: string
            }
            
            status: #Status1 | #Status2
            """);

        using var statusField = value.Lookup("status");
        var node = statusField.ToCueValueNode();

        Assert.IsType<CueDisjunction>(node);
        var discriminator = (CueDisjunction)node;
        
        Assert.All(discriminator.Branches, branch =>
        {
            var structBranch = Assert.IsType<CueStructValue>(branch);
            Assert.True(structBranch.Fields.Count > 0);
            Assert.Contains(structBranch.Fields, f => f.Name == "type");
        });
    }

    [Fact]
    public void DiscriminatorUnionBranchesHaveDifferentDiscriminatorValues()
    {
        using var ctx = new CueContext();
        using var value = ctx.Compile("""
            #FormatA: {type: "date", pattern: string}
            #FormatB: {type: "time", pattern: string}
            
            format: #FormatA | #FormatB
            """);

        using var formatField = value.Lookup("format");
        var node = formatField.ToCueValueNode();

        Assert.IsType<CueDisjunction>(node);
        var discriminator = (CueDisjunction)node;
        
        // Extract discriminator values from each branch
        var discriminatorValues = new HashSet<string>();
        foreach (var branch in discriminator.Branches)
        {
            var structBranch = Assert.IsType<CueStructValue>(branch);
            var typeField = structBranch.Fields.First(f => f.Name == "type");
            Assert.True(typeField.Value is CueStringValue);
            discriminatorValues.Add(typeField.Value.Path);
        }
        
        // Should have unique values for each branch
        Assert.Equal(discriminator.Branches.Count, discriminatorValues.Count);
    }

    [Fact]
    public void DiscriminatorUnionInNestedStruct()
    {
        using var ctx = new CueContext();
        using var value = ctx.Compile("""
            #FormatA: {type: "a", value: string}
            #FormatB: {type: "b", value: int}
            
            config: {
                name: string
                format: #FormatA | #FormatB
            }
            """);

        var node = (CueStructValue)value.ToCueValueNode();
        var configField = node.Fields.First(f => f.Name == "config");
        var configStruct = (CueStructValue)configField.Value;
        var formatField = configStruct.Fields.First(f => f.Name == "format");

        Assert.IsType<CueDisjunction>(formatField.Value);
        var disjunction = (CueDisjunction)formatField.Value;
        Assert.True(disjunction.IsDiscriminated);
    }

    [Fact]
    public void DiscriminatorUnionInList()
    {
        using var ctx = new CueContext();
        using var value = ctx.Compile("""
            #ItemA: {type: "a", data: string}
            #ItemB: {type: "b", data: int}
            
            items: [#ItemA | #ItemB]
            """);

        var node = (CueStructValue)value.ToCueValueNode();
        var itemsField = node.Fields.First(f => f.Name == "items");
        var listValue = (CueListValue)itemsField.Value;

        Assert.IsType<CueDisjunction>(listValue.ElementType);
        var disjunction = (CueDisjunction)listValue.ElementType;
        Assert.True(disjunction.IsDiscriminated);
    }

    [Fact]
    public void ComplexDiscriminatorUnionExample()
    {
        using var ctx = new CueContext();
        using var value = ctx.Compile("""
            #DateTimeField: {
                type: "datetime"
                format: string
                timezone?: string
            }
            #TextLineField: {
                type: "text"
                maxLength: int
                pattern?: string
            }
            #InitialsField: {
                type: "initials"
                maxInitials: int
            }
            
            field: #DateTimeField | #TextLineField | #InitialsField
            """);

        using var fieldProp = value.Lookup("field");
        var node = fieldProp.ToCueValueNode();

        Assert.IsType<CueDisjunction>(node);
        var discriminator = (CueDisjunction)node;
        
        Assert.True(discriminator.IsDiscriminated);
        Assert.Equal(3, discriminator.Branches.Count);
        Assert.Equal("type", discriminator.DiscriminatorField);
        
        var types = new HashSet<string>();
        foreach (var branch in discriminator.Branches)
        {
            var structBranch = Assert.IsType<CueStructValue>(branch);
            var typeField = structBranch.Fields.First(f => f.Name == "type");
            types.Add(typeField.Value.Path);
        }
        Assert.Equal(3, types.Count);
    }

    [Fact]
    public void DiscriminatorUnionPrefersTypeField()
    {
        // If multiple fields could be discriminators, 'type' should be preferred
        using var ctx = new CueContext();
        using var value = ctx.Compile("""
            #OptionA: {type: "a", kind: "kindA"}
            #OptionB: {type: "b", kind: "kindB"}
            
            choice: #OptionA | #OptionB
            """);

        using var choiceField = value.Lookup("choice");
        var node = choiceField.ToCueValueNode();

        Assert.IsType<CueDisjunction>(node);
        var discriminator = (CueDisjunction)node;
        
        // Should prefer 'type' over 'kind'
        Assert.True(discriminator.IsDiscriminated);
        Assert.Equal("type", discriminator.DiscriminatorField);
    }

    [Fact]
    public void RealWorldAnnotationElementExample()
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
            
            element: #AnnotationElement
            """);

        var node = (CueStructValue)value.ToCueValueNode();
        var elementField = node.Fields.First(f => f.Name == "element");
        var elementStruct = (CueStructValue)elementField.Value;
        var valueFormatField = elementStruct.Fields.First(f => f.Name == "valueFormat");

        Assert.IsType<CueDisjunction>(valueFormatField.Value);
        var discriminator = (CueDisjunction)valueFormatField.Value;
        
        Assert.True(discriminator.IsDiscriminated);
        Assert.Equal(3, discriminator.Branches.Count);
        Assert.Equal("type", discriminator.DiscriminatorField);
    }

    [Fact]
    public void DiscriminatorUnionPathContainsFieldName()
    {
        using var ctx = new CueContext();
        using var value = ctx.Compile("""
            #TypeA: {type: "a"}
            #TypeB: {type: "b"}
            
            config: {
                variant: #TypeA | #TypeB
            }
            """);

        using var config = value.Lookup("config");
        using var variantField = config.Lookup("variant");
        var node = variantField.ToCueValueNode();

        Assert.IsType<CueDisjunction>(node);
        var discriminator = (CueDisjunction)node;
        
        Assert.Contains("variant", discriminator.Path);
    }

    [Fact]
    public void CanConvertToValueNodeAndBackMultipleTimes()
    {
        using var ctx = new CueContext();
        using var value1 = ctx.Compile("""
            #TypeA: {type: "a", data: string}
            #TypeB: {type: "b", data: int}
            
            item: #TypeA | #TypeB
            """);

        using var itemField1 = value1.Lookup("item");
        var node1 = itemField1.ToCueValueNode();
        
        Assert.IsType<CueDisjunction>(node1);
        
        // Convert again with a fresh context
        using var ctx2 = new CueContext();
        using var value2 = ctx2.Compile("""
            #TypeA: {type: "a", data: string}
            #TypeB: {type: "b", data: int}
            
            item: #TypeA | #TypeB
            """);

        using var itemField2 = value2.Lookup("item");
        var node2 = itemField2.ToCueValueNode();
        
        Assert.IsType<CueDisjunction>(node2);
        var d1 = (CueDisjunction)node1;
        var d2 = (CueDisjunction)node2;
        
        Assert.Equal(d1.DiscriminatorField, d2.DiscriminatorField);
        Assert.Equal(d1.Branches.Count, d2.Branches.Count);
    }
}

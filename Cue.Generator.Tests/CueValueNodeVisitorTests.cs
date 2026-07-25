namespace Cue.Generator.Tests;

public sealed class CueValueNodeVisitorTests
{
    [Fact]
    public void VisitSimpleNullValue()
    {
        using var ctx = new CueContext();
        using var value = ctx.Compile("null");
        var node = value.ToCueValueNode();

        Assert.NotNull(node);
        Assert.IsType<CueSimpleValue>(node);
        Assert.Equal(Kind.Null, node.Kind);
        Assert.Equal("", node.Path);
    }

    [Fact]
    public void VisitSimpleBoolValue()
    {
        using var ctx = new CueContext();
        using var value = ctx.Compile("bool");
        var node = value.ToCueValueNode();

        Assert.NotNull(node);
        Assert.IsType<CueSimpleValue>(node);
        Assert.Equal(Kind.Bool, node.Kind);
    }

    [Fact]
    public void VisitSimpleIntValue()
    {
        using var ctx = new CueContext();
        using var value = ctx.Compile("1");
        var node = value.ToCueValueNode();

        Assert.NotNull(node);
        Assert.IsType<CueSimpleValue>(node);
        Assert.Equal(Kind.Int, node.Kind);
    }

    [Fact]
    public void VisitSimpleFloatValue()
    {
        using var ctx = new CueContext();
        using var value = ctx.Compile("1.2");
        var node = value.ToCueValueNode();

        Assert.NotNull(node);
        Assert.IsType<CueSimpleValue>(node);
        Assert.Equal(Kind.Float, node.Kind);
    }

    [Fact]
    public void VisitSimpleStringValue()
    {
        using var ctx = new CueContext();
        using var value = ctx.Compile("\"hello\"");
        var node = value.ToCueValueNode();

        Assert.NotNull(node);
        Assert.IsType<CueSimpleValue>(node);
        Assert.Equal(Kind.String, node.Kind);
    }

    [Fact]
    public void VisitBytesValue()
    {
        using var ctx = new CueContext();
        using var value = ctx.Compile("'\\xde\\xad\\xbe\\xef'");
        var node = value.ToCueValueNode();

        Assert.NotNull(node);
        Assert.IsType<CueSimpleValue>(node);
        Assert.Equal(Kind.Bytes, node.Kind);
    }

    [Fact]
    public void VisitSimpleStructWithPrimitiveFields()
    {
        using var ctx = new CueContext();
        using var value = ctx.Compile("""
            x: 1
            y: true
            z: "text"
            """);
        var node = value.ToCueValueNode();

        Assert.NotNull(node);
        Assert.IsType<CueStructValue>(node);
        var structNode = (CueStructValue)node;
        Assert.Equal(3, structNode.Fields.Count);
        
        var fieldNames = structNode.Fields.Select(f => f.Name).OrderBy(n => n).ToList();
        Assert.Equal(new[] { "x", "y", "z" }, fieldNames);
        
        Assert.All(structNode.Fields, field => 
        {
            Assert.NotNull(field.Value);
            Assert.IsType<CueSimpleValue>(field.Value);
        });
    }

    [Fact]
    public void VisitNestedStruct()
    {
        using var ctx = new CueContext();
        using var value = ctx.Compile("""
            person: {
                name: "Jane"
                age: 30
            }
            """);
        var node = value.ToCueValueNode();

        Assert.NotNull(node);
        Assert.IsType<CueStructValue>(node);
        var rootStruct = (CueStructValue)node;
        
        var personField = rootStruct.Fields.First(f => f.Name == "person");
        Assert.NotNull(personField);
        Assert.IsType<CueStructValue>(personField.Value);
        
        var personStruct = (CueStructValue)personField.Value;
        Assert.Equal(2, personStruct.Fields.Count);
    }

    [Fact]
    public void VisitDeeplyNestedStruct()
    {
        using var ctx = new CueContext();
        using var value = ctx.Compile("""
            a: {
                b: {
                    c: {
                        d: "deep"
                    }
                }
            }
            """);
        var node = value.ToCueValueNode();

        Assert.NotNull(node);
        Assert.IsType<CueStructValue>(node);
        
        var current = node as CueStructValue;
        for (int i = 0; i < 4; i++)
        {
            Assert.NotNull(current);
            Assert.Single(current.Fields);
            current = current.Fields[0].Value as CueStructValue;
        }
    }

    [Fact]
    public void VisitListWithSimpleElements()
    {
        using var ctx = new CueContext();
        using var value = ctx.Compile("[1, 2, 3]");
        var node = value.ToCueValueNode();

        Assert.NotNull(node);
        Assert.IsType<CueListValue>(node);
        var listNode = (CueListValue)node;
        Assert.IsType<CueSimpleValue>(listNode.ElementType);
        Assert.Equal(Kind.Int, listNode.ElementType.Kind);
    }

    [Fact]
    public void VisitListSchema()
    {
        using var ctx = new CueContext();
        using var value = ctx.Compile("[...string]");
        var node = value.ToCueValueNode();

        Assert.NotNull(node);
        Assert.IsType<CueListValue>(node);
        var listNode = (CueListValue)node;
        Assert.IsType<CueSimpleValue>(listNode.ElementType);
        Assert.Equal(Kind.String, listNode.ElementType.Kind);
    }

    [Fact]
    public void VisitStructContainingList()
    {
        using var ctx = new CueContext();
        using var value = ctx.Compile("""
            items: [1, 2, 3]
            count: 3
            """);
        var node = value.ToCueValueNode();

        Assert.NotNull(node);
        Assert.IsType<CueStructValue>(node);
        var structNode = (CueStructValue)node;
        
        var itemsField = structNode.Fields.First(f => f.Name == "items");
        Assert.IsType<CueListValue>(itemsField.Value);
    }

    [Fact]
    public void VisitListOfStructs()
    {
        using var ctx = new CueContext();
        using var value = ctx.Compile("""
            [{x: 1}, {x: 2}]
            """);
        var node = value.ToCueValueNode();

        Assert.NotNull(node);
        Assert.IsType<CueListValue>(node);
        var listNode = (CueListValue)node;
        Assert.IsType<CueStructValue>(listNode.ElementType);
    }

    [Fact]
    public void VisitDisjunctionOfTypes()
    {
        using var ctx = new CueContext();
        using var value = ctx.Compile("int | string | bool");
        var node = value.ToCueValueNode();

        Assert.NotNull(node);
        Assert.IsType<CueDisjunction>(node);
        var disjunction = (CueDisjunction)node;
        
        Assert.Equal(3, disjunction.Branches.Count);
        var kinds = disjunction.Branches.Select(b => b.Kind).OrderBy(k => (int)k).ToList();
        Assert.Contains(Kind.Bool, kinds);
        Assert.Contains(Kind.Int, kinds);
        Assert.Contains(Kind.String, kinds);
    }

    [Fact]
    public void VisitDisjunctionOfValues()
    {
        using var ctx = new CueContext();
        using var value = ctx.Compile("1 | 2 | 3");
        var node = value.ToCueValueNode();

        Assert.NotNull(node);
        Assert.IsType<CueDisjunction>(node);
        var disjunction = (CueDisjunction)node;
        
        Assert.Equal(3, disjunction.Branches.Count);
        Assert.All(disjunction.Branches, b => Assert.Equal(Kind.Int, b.Kind));
    }

    [Fact]
    public void VisitValueWithDefault()
    {
        using var ctx = new CueContext();
        using var value = ctx.Compile("int | *1");
        var node = value.ToCueValueNode();

        Assert.NotNull(node);
        Assert.IsType<CueDisjunction>(node);
    }

    [Fact]
    public void VisitConstrainedInt()
    {
        using var ctx = new CueContext();
        using var value = ctx.Compile(">0 & <100");
        var node = value.ToCueValueNode();

        Assert.NotNull(node);
        Assert.IsType<CueSimpleValue>(node);
        Assert.Equal(Kind.Int, node.Kind);
    }

    [Fact]
    public void VisitStructSchemaWithConstraints()
    {
        using var ctx = new CueContext();
        using var value = ctx.Compile("""
            {
                name: string
                age: >0 & <150
                email: string & =~"^.*@.*"
            }
            """);
        var node = value.ToCueValueNode();

        Assert.NotNull(node);
        Assert.IsType<CueStructValue>(node);
        var structNode = (CueStructValue)node;
        Assert.Equal(3, structNode.Fields.Count);
    }

    [Fact]
    public void VisitOptionalField()
    {
        using var ctx = new CueContext();
        using var value = ctx.Compile("""
            {
                required: string
                optional?: int
            }
            """);
        var node = value.ToCueValueNode();

        Assert.NotNull(node);
        Assert.IsType<CueStructValue>(node);
        var structNode = (CueStructValue)node;
        
        var fieldNames = structNode.Fields.Select(f => f.Name).OrderBy(n => n).ToList();
        Assert.Equal(2, fieldNames.Count);
    }

    [Fact]
    public void VisitMapSchema()
    {
        using var ctx = new CueContext();
        using var value = ctx.Compile("[string]: int");
        var node = value.ToCueValueNode();

        Assert.NotNull(node);
        Assert.IsType<CueStructValue>(node);
    }

    [Fact]
    public void VisitComplexNestedStructure()
    {
        using var ctx = new CueContext();
        using var value = ctx.Compile("""
            users: [
                {
                    name: "Alice"
                    roles: ["admin", "user"]
                    metadata: {
                        created: "2024-01-01"
                        tags: ["important"]
                    }
                }
            ]
            count: 1
            """);
        var node = value.ToCueValueNode();

        Assert.NotNull(node);
        Assert.IsType<CueStructValue>(node);
        var rootStruct = (CueStructValue)node;
        
        Assert.Equal(2, rootStruct.Fields.Count);
        
        var usersField = rootStruct.Fields.First(f => f.Name == "users");
        Assert.IsType<CueListValue>(usersField.Value);
    }

    [Fact]
    public void VisitUnifiedStructs()
    {
        using var ctx = new CueContext();
        using var schema = ctx.Compile("a: int\nb: string");
        using var concrete = ctx.Compile("a: 42\nb: \"hello\"");
        using var unified = schema.Unify(concrete);
        
        var node = unified.ToCueValueNode();

        Assert.NotNull(node);
        Assert.IsType<CueStructValue>(node);
        var structNode = (CueStructValue)node;
        
        Assert.Equal(2, structNode.Fields.Count);
        var fieldNames = structNode.Fields.Select(f => f.Name).OrderBy(n => n).ToList();
        Assert.Equal(new[] { "a", "b" }, fieldNames);
    }

    [Fact]
    public void VisitLookupedValue()
    {
        using var ctx = new CueContext();
        using var root = ctx.Compile("""
            person: {
                name: "Jane"
                age: 30
            }
            """);
        using var person = root.Lookup("person");
        var node = person.ToCueValueNode();

        Assert.NotNull(node);
        Assert.IsType<CueStructValue>(node);
        var structNode = (CueStructValue)node;
        Assert.Equal(2, structNode.Fields.Count);
    }

    [Fact]
    public void VisitEmptyStruct()
    {
        using var ctx = new CueContext();
        using var value = ctx.Compile("{}");
        var node = value.ToCueValueNode();

        Assert.NotNull(node);
        Assert.IsType<CueStructValue>(node);
        var structNode = (CueStructValue)node;
        Assert.Empty(structNode.Fields);
    }

    [Fact]
    public void VisitEmptyList()
    {
        using var ctx = new CueContext();
        using var value = ctx.Compile("[]");
        var node = value.ToCueValueNode();

        Assert.NotNull(node);
        Assert.IsType<CueListValue>(node);
    }

    [Fact]
    public void VisitMixedDisjunctionWithStructAndList()
    {
        using var ctx = new CueContext();
        using var value = ctx.Compile("""
            {
                options: [string] | string
            }
            """);
        var node = value.ToCueValueNode();

        Assert.NotNull(node);
        Assert.IsType<CueStructValue>(node);
    }

    [Fact]
    public void VisitToValueConvertedInt()
    {
        using var ctx = new CueContext();
        using var value = ctx.ToValue(42);
        var node = value.ToCueValueNode();

        Assert.NotNull(node);
        Assert.IsType<CueSimpleValue>(node);
        Assert.Equal(Kind.Int, node.Kind);
    }

    [Fact]
    public void VisitToValueConvertedBool()
    {
        using var ctx = new CueContext();
        using var value = ctx.ToValue(true);
        var node = value.ToCueValueNode();

        Assert.NotNull(node);
        Assert.IsType<CueSimpleValue>(node);
        Assert.Equal(Kind.Bool, node.Kind);
    }

    [Fact]
    public void VisitToValueConvertedString()
    {
        using var ctx = new CueContext();
        using var value = ctx.ToValue("test");
        var node = value.ToCueValueNode();

        Assert.NotNull(node);
        Assert.IsType<CueSimpleValue>(node);
        Assert.Equal(Kind.String, node.Kind);
    }

    [Fact]
    public void VisitToValueConvertedDouble()
    {
        using var ctx = new CueContext();
        using var value = ctx.ToValue(3.14);
        var node = value.ToCueValueNode();

        Assert.NotNull(node);
        Assert.IsType<CueSimpleValue>(node);
        Assert.Equal(Kind.Float, node.Kind);
    }

    [Fact]
    public void VisitToValueConvertedBytes()
    {
        using var ctx = new CueContext();
        using var value = ctx.ToValue(new byte[] { 1, 2, 3 });
        var node = value.ToCueValueNode();

        Assert.NotNull(node);
        Assert.IsType<CueSimpleValue>(node);
        Assert.Equal(Kind.Bytes, node.Kind);
    }

    [Fact]
    public void PathIsPreservedForRootValue()
    {
        using var ctx = new CueContext();
        using var value = ctx.Compile("42");
        var node = value.ToCueValueNode();

        Assert.Equal("", node.Path);
    }

    [Fact]
    public void PathIsPreservedForNestedField()
    {
        using var ctx = new CueContext();
        using var root = ctx.Compile("""
            person: {
                name: "Jane"
            }
            """);
        using var person = root.Lookup("person");
        using var name = person.Lookup("name");
        var node = name.ToCueValueNode();

        Assert.Equal("person.name", node.Path);
    }

    [Fact]
    public void PathIsPreservedForListElement()
    {
        using var ctx = new CueContext();
        using var root = ctx.Compile("""
            items: [1, 2, 3]
            """);
        using var items = root.Lookup("items");
        var node = items.ToCueValueNode();

        Assert.Equal("items", node.Path);
    }

    [Fact]
    public void StructFieldNamesAreCorrect()
    {
        using var ctx = new CueContext();
        using var value = ctx.Compile("""
            firstName: "John"
            lastName: "Doe"
            age: 30
            """);
        var node = (CueStructValue)value.ToCueValueNode();

        var fieldNames = node.Fields.Select(f => f.Name).OrderBy(n => n).ToList();
        Assert.Equal(new[] { "age", "firstName", "lastName" }, fieldNames);
    }

    [Fact]
    public void NestedStructFieldNamesAreCorrect()
    {
        using var ctx = new CueContext();
        using var value = ctx.Compile("""
            user: {
                firstName: "Jane"
                contact: {
                    email: "jane@example.com"
                    phone: "123-456-7890"
                }
            }
            """);
        using var user = value.Lookup("user");
        var node = (CueStructValue)user.ToCueValueNode();

        var userFields = node.Fields.Select(f => f.Name).OrderBy(n => n).ToList();
        Assert.Contains("contact", userFields);
        Assert.Contains("firstName", userFields);
    }

    [Fact]
    public void DisjunctionPreservesPath()
    {
        using var ctx = new CueContext();
        using var value = ctx.Compile("""
            value: int | string | bool
            """);
        using var valueField = value.Lookup("value");
        var node = valueField.ToCueValueNode();

        Assert.Equal("value", node.Path);
    }

    [Fact]
    public void VisitStructWithDifferentFieldTypes()
    {
        using var ctx = new CueContext();
        using var value = ctx.Compile("""
            intField: 42
            floatField: 3.14
            stringField: "text"
            boolField: true
            nullField: null
            listField: [1, 2, 3]
            structField: {x: 1}
            """);
        var node = (CueStructValue)value.ToCueValueNode();

        Assert.Equal(7, node.Fields.Count);
        
        var intField = node.Fields.First(f => f.Name == "intField");
        Assert.IsType<CueSimpleValue>(intField.Value);
        Assert.Equal(Kind.Int, intField.Value.Kind);
        
        var listField = node.Fields.First(f => f.Name == "listField");
        Assert.IsType<CueListValue>(listField.Value);
        
        var structField = node.Fields.First(f => f.Name == "structField");
        Assert.IsType<CueStructValue>(structField.Value);
    }

    [Fact]
    public void VisitComplexRealWorldExample()
    {
        using var ctx = new CueContext();
        using var value = ctx.Compile("""
            apiVersion: "v1"
            kind: "Service"
            metadata: {
                name: "my-service"
                namespace: "default"
                labels: {
                    app: "myapp"
                }
            }
            spec: {
                selector: {
                    app: "myapp"
                }
                ports: [{
                    protocol: "TCP"
                    port: 80
                    targetPort: 8080
                }]
                type: "ClusterIP"
            }
            """);
        var node = value.ToCueValueNode();

        Assert.NotNull(node);
        Assert.IsType<CueStructValue>(node);
        var rootStruct = (CueStructValue)node;
        
        Assert.True(rootStruct.Fields.Count >= 4);
        Assert.Contains(rootStruct.Fields, f => f.Name == "metadata");
        Assert.Contains(rootStruct.Fields, f => f.Name == "spec");
    }

    [Fact]
    public void VisitTopKind()
    {
        using var ctx = new CueContext();
        using var value = ctx.Top();
        var node = value.ToCueValueNode();

        Assert.NotNull(node);
        Assert.IsType<CueSimpleValue>(node);
        Assert.Equal(Kind.Top, node.Kind);
    }

    [Fact]
    public void VisitIntType()
    {
        using var ctx = new CueContext();
        using var value = ctx.Compile("int");
        var node = value.ToCueValueNode();

        Assert.NotNull(node);
        Assert.IsType<CueSimpleValue>(node);
        Assert.Equal(Kind.Int, node.Kind);
    }

    [Fact]
    public void VisitStringType()
    {
        using var ctx = new CueContext();
        using var value = ctx.Compile("string");
        var node = value.ToCueValueNode();

        Assert.NotNull(node);
        Assert.IsType<CueSimpleValue>(node);
        Assert.Equal(Kind.String, node.Kind);
    }

    [Fact]
    public void VisitJsonValue()
    {
        using var ctx = new CueContext();
        using var value = ctx.Compile("""
            {
                "key": "value"
                "number": 123
                "nested": {
                    "deep": true
                }
            }
            """);
        var node = value.ToCueValueNode();

        Assert.NotNull(node);
        Assert.IsType<CueStructValue>(node);
    }

    [Fact]
    public void VisitMultipleDisjunctionsInStruct()
    {
        using var ctx = new CueContext();
        using var value = ctx.Compile("""
            status: "active" | "inactive" | "pending"
            priority: 1 | 2 | 3
            enabled: bool
            """);
        var node = (CueStructValue)value.ToCueValueNode();

        Assert.NotNull(node);
        Assert.Equal(3, node.Fields.Count);
        
        var statusField = node.Fields.First(f => f.Name == "status");
        Assert.IsType<CueDisjunction>(statusField.Value);
    }
}

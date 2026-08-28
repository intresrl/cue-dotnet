namespace Cue.Generator.Tests.CueValueNodeVisitor;

public sealed class CueValueNodeVisitorTests
{
    private static Kind GetKind(CueValueNode node)
    {
        return node switch
        {
            CueBottomValue => Kind.Bottom,
            CueNullValue => Kind.Null,
            CueBoolValue => Kind.Bool,
            CueIntValue => Kind.Int,
            CueFloatValue => Kind.Float,
            CueStringValue => Kind.String,
            CueBytesValue => Kind.Bytes,
            CueNumberValue => Kind.Number,
            CueTopValue => Kind.Top,
            CueStructValue => Kind.Struct,
            CueListValue => Kind.List,
            _ => throw new  ArgumentOutOfRangeException(nameof(node), node, null)
        };
    }
    
    [Fact]
    public void VisitListWithSimpleElements()
    {
        using var ctx = new CueContext();
        using var value = ctx.Compile("[...(1 | 2 | 3)]");
        var node = CueValueVisitor.ForTests(value);

        Assert.NotNull(node);
        var listNode = Assert.IsType<CueListValue>(node);
        Assert.Equal(Kind.Int, GetKind(listNode.Tail));
    }

    [Fact]
    public void VisitListSchema()
    {
        using var ctx = new CueContext();
        using var value = ctx.Compile("[...string]");
        var node = CueValueVisitor.ForTests(value);

        Assert.NotNull(node);
        var listNode = Assert.IsType<CueListValue>(node);
        Assert.Equal(Kind.String, GetKind(listNode.Tail));
    }

    [Fact]
    public void VisitFixedListCapturesIndexedElementsWithoutAnyIndex()
    {
        using var ctx = new CueContext();
        using var value = ctx.Compile("[string, int, bool]");
        var listNode = Assert.IsType<CueListValue>(CueValueVisitor.ForTests(value));

        Assert.Null(listNode.Tail);
        Assert.Collection(
            listNode.Indexed,
            value => Assert.Equal(Kind.String, GetKind(value)),
            value => Assert.Equal(Kind.Int, GetKind(value)),
            value => Assert.Equal(Kind.Bool, GetKind(value)));
    }

    [Fact]
    public void VisitMixedListCapturesIndexedElementsAndAnyIndex()
    {
        using var ctx = new CueContext();
        using var value = ctx.Compile("[string, int, ...bool]");
        var listNode = Assert.IsType<CueListValue>(CueValueVisitor.ForTests(value));

        Assert.Equal(Kind.Bool, GetKind(Assert.IsType<CueBoolValue>(listNode.Tail)));
        Assert.Collection(
            listNode.Indexed,
            value => Assert.Equal(Kind.String, GetKind(value)),
            value => Assert.Equal(Kind.Int, GetKind(value)));
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
        var node = CueValueVisitor.ForTests(value);

        Assert.NotNull(node);
        Assert.IsType<CueStructValue>(node);
        var structNode = (CueStructValue)node;
        Assert.Equal(3, structNode.Fields.Count);

        var fieldNames = structNode.Fields.Select(f => f.Name).OrderBy(n => n).ToList();
        Assert.Equal(["x", "y", "z"], fieldNames);

        Assert.All(structNode.Fields, field =>
        {
            Assert.NotNull(field.Value);
            Assert.True(GetKind(field.Value) is not (Kind.Struct or Kind.List));
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
        var node = CueValueVisitor.ForTests(value);

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
        var node = CueValueVisitor.ForTests(value);

        Assert.NotNull(node);
        Assert.IsType<CueStructValue>(node);

        var current = node as CueStructValue;
        for (var i = 0; i < 4; i++)
        {
            Assert.NotNull(current);
            Assert.Single(current.Fields);
            current = current.Fields[0].Value as CueStructValue;
        }
    }

    [Fact]
    public void VisitStructContainingList()
    {
        using var ctx = new CueContext();
        using var value = ctx.Compile("""
                                      items: [...(1 | 2 | 3)]
                                      count: 3
                                      """);
        var node = CueValueVisitor.ForTests(value);

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
        using var value = ctx.Compile("[...{x: int}]");
        var node = CueValueVisitor.ForTests(value);

        Assert.NotNull(node);
        Assert.IsType<CueListValue>(node);
        var listNode = (CueListValue)node;
        Assert.IsType<CueStructValue>(listNode.Tail);
    }

    [Fact]
    public void VisitDisjunctionOfTypes()
    {
        using var ctx = new CueContext();
        using var value = ctx.Compile("x: int | string | bool");
        var node = CueValueVisitor.ForTests(value.Lookup("x"));

        Assert.NotNull(node);
        Assert.IsType<CueDisjunction>(node);
        var disjunction = (CueDisjunction)node;

        Assert.Equal(3, disjunction.Branches.Count);
        var kinds = disjunction.Branches.Select(GetKind).OrderBy(k => (int)k).ToList();
        Assert.Contains(Kind.Bool, kinds);
        Assert.Contains(Kind.Int, kinds);
        Assert.Contains(Kind.String, kinds);
    }

    [Fact]
    public void VisitDisjunctionOfValuesOfSameSimpleType_IsSimpleType()
    {
        using var ctx = new CueContext();
        using var value = ctx.Compile("x: 1 | 2 | 3");
        var node = CueValueVisitor.ForTests(value.Lookup("x"));

        Assert.NotNull(node);
        Assert.IsType<CueIntValue>(node);
    }
    
    [Fact(Skip = "Disjunctions in root do not work")]
    public void VisitDisjunctionInRoot_Works()
    {
        using var ctx = new CueContext();
        using var value = ctx.Compile("1 | 2 | 3");
        var node = CueValueVisitor.ForTests(value);

        Assert.NotNull(node);
        Assert.IsType<CueIntValue>(node);
    }
    
    [Fact(Skip = "list with positioned elements do not work")]
    public void VisitListWithPositionedElements_Works()
    {
        using var ctx = new CueContext();
        using var value = ctx.Compile("x: [1, 2, 3]");
        Assert.IsType<CueIntValue>(CueValueVisitor.ForTests(value.Lookup("x[0]")));
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
        var node = CueValueVisitor.ForTests(value);

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
        var node = CueValueVisitor.ForTests(value);

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
        var node = CueValueVisitor.ForTests(value);

        Assert.NotNull(node);
        Assert.IsType<CueStructValue>(node);
    }

    [Fact]
    public void VisitComplexNestedStructure()
    {
        using var ctx = new CueContext();
        using var value = ctx.Compile("""
                                      users: [
                                          ...{
                                              name: "Alice"
                                              roles: [ ...("admin" | "user") ]
                                              metadata: {
                                                  created: "2024-01-01"
                                                  tags: [...("important")]
                                              }
                                          }
                                      ]
                                      count: 1
                                      """);
        var node = CueValueVisitor.ForTests(value);

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

        var node = CueValueVisitor.ForTests(unified);

        Assert.NotNull(node);
        Assert.IsType<CueStructValue>(node);
        var structNode = (CueStructValue)node;

        Assert.Equal(2, structNode.Fields.Count);
        var fieldNames = structNode.Fields.Select(f => f.Name).OrderBy(n => n).ToList();
        Assert.Equal(["a", "b"], fieldNames);
    }

    [Fact]
    public void VisitLookupValue()
    {
        using var ctx = new CueContext();
        using var root = ctx.Compile("""
                                     person: {
                                         name: "Jane"
                                         age: 30
                                     }
                                     """);
        using var person = root.Lookup("person");
        var node = CueValueVisitor.ForTests(person);

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
        var node = CueValueVisitor.ForTests(value);

        Assert.NotNull(node);
        Assert.IsType<CueStructValue>(node);
        var structNode = (CueStructValue)node;
        Assert.Empty(structNode.Fields);
    }
    
    [Fact]
    public void VisitMixedDisjunctionWithStructAndList()
    {
        using var ctx = new CueContext();
        using var value = ctx.Compile("""
                                      {
                                          options: [...string] | string
                                      }
                                      """);
        var node = CueValueVisitor.ForTests(value);

        Assert.NotNull(node);
        Assert.IsType<CueStructValue>(node);
    }

    [Fact]
    public void VisitToValueConvertedInt()
    {
        using var ctx = new CueContext();
        using var value = ctx.ToValue(42);
        var node = CueValueVisitor.ForTests(value);

        Assert.NotNull(node);
        Assert.Equal(Kind.Int, GetKind(node));
    }

    [Fact]
    public void VisitToValueConvertedBool()
    {
        using var ctx = new CueContext();
        using var value = ctx.ToValue(true);
        var node = CueValueVisitor.ForTests(value);

        Assert.NotNull(node);
        Assert.Equal(Kind.Bool, GetKind(node));
    }

    [Fact]
    public void VisitToValueConvertedString()
    {
        using var ctx = new CueContext();
        using var value = ctx.ToValue("test");
        var node = CueValueVisitor.ForTests(value);

        Assert.NotNull(node);
        Assert.Equal(Kind.String, GetKind(node));
    }

    [Fact]
    public void VisitToValueConvertedDouble()
    {
        using var ctx = new CueContext();
        using var value = ctx.ToValue(3.14);
        var node = CueValueVisitor.ForTests(value);

        Assert.NotNull(node);
        Assert.Equal(Kind.Float, GetKind(node));
    }

    [Fact]
    public void VisitToValueConvertedBytes()
    {
        using var ctx = new CueContext();
        using var value = ctx.ToValue([1, 2, 3]);
        var node = CueValueVisitor.ForTests(value);

        Assert.NotNull(node);
        Assert.Equal(Kind.Bytes, GetKind(node));
    }

    [Fact]
    public void PathIsPreservedForRootValue()
    {
        using var ctx = new CueContext();
        using var value = ctx.Compile("42");
        var node = CueValueVisitor.ForTests(value);

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
        var node = CueValueVisitor.ForTests(name);

        Assert.Equal("person.name", node.Path);
    }

    [Fact]
    public void PathIsPreservedForListElement()
    {
        using var ctx = new CueContext();
        using var root = ctx.Compile("items: [ ...(1 | 2 | 3) ]");
        using var items = root.Lookup("items");
        var node = CueValueVisitor.ForTests(items);

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
        var node = (CueStructValue)CueValueVisitor.ForTests(value);

        var fieldNames = node.Fields.Select(f => f.Name).OrderBy(n => n).ToList();
        Assert.Equal(["age", "firstName", "lastName"], fieldNames);
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
        var node = (CueStructValue)CueValueVisitor.ForTests(user);

        var userFields = node.Fields.Select(f => f.Name).OrderBy(n => n).ToList();
        Assert.Contains("contact", userFields);
        Assert.Contains("firstName", userFields);
    }

    [Fact]
    public void DisjunctionPreservesPath()
    {
        using var ctx = new CueContext();
        using var value = ctx.Compile("value: int | string | bool");
        using var valueField = value.Lookup("value");
        var node = CueValueVisitor.ForTests(valueField);

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
                                      listField: [...(1 | 2 | 3)]
                                      structField: {x: 1}
                                      """);
        var node = (CueStructValue)CueValueVisitor.ForTests(value);

        Assert.Equal(7, node.Fields.Count);

        var intField = node.Fields.First(f => f.Name == "intField");
        Assert.Equal(Kind.Int, GetKind(intField.Value));

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
                                          ports: [...{
                                              protocol: "TCP"
                                              port: 80
                                              targetPort: 8080
                                          }]
                                          type: "ClusterIP"
                                      }
                                      """);
        var node = CueValueVisitor.ForTests(value);

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
        var node = CueValueVisitor.ForTests(value);

        Assert.NotNull(node);
        Assert.Equal(Kind.Top, GetKind(node));
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
        var node = CueValueVisitor.ForTests(value);

        Assert.NotNull(node);
        Assert.IsType<CueStructValue>(node);
    }

    [Fact]
    public void VisitMultipleDisjunctionsInStructWithSameSimpleType_HaveSimpleType()
    {
        using var ctx = new CueContext();
        using var value = ctx.Compile("""
                                      status: "active" | "inactive" | "pending"
                                      priority: 1 | 2 | 3
                                      enabled: bool
                                      """);
        var node = (CueStructValue)CueValueVisitor.ForTests(value);

        Assert.NotNull(node);
        Assert.Equal(3, node.Fields.Count);

        var statusField = node.Fields.First(f => f.Name == "status");
        Assert.IsType<CueStringValue>(statusField.Value);
    }

    [Fact]
    public void ConcreteValuesInStructFieldsAreExtracted()
    {
        using var ctx = new CueContext();
        using var value = ctx.Compile("""
                                      name: "Alice"
                                      age: 30
                                      active: true
                                      score: 95.5
                                      """);
        var node = (CueStructValue)CueValueVisitor.ForTests(value);

        Assert.NotNull(node);
        Assert.Equal(4, node.Fields.Count);

        var nameField = node.Fields.First(f => f.Name == "name");
        var nameNode = Assert.IsType<CueStringValue>(nameField.Value);
        Assert.Equal("Alice", nameNode.ConcreteValue);

        var ageField = node.Fields.First(f => f.Name == "age");
        var ageNode = Assert.IsType<CueIntValue>(ageField.Value);
        Assert.Equal(30, ageNode.ConcreteValue);

        var activeField = node.Fields.First(f => f.Name == "active");
        var activeNode = Assert.IsType<CueBoolValue>(activeField.Value);
        Assert.True(activeNode.ConcreteValue);

        var scoreField = node.Fields.First(f => f.Name == "score");
        var scoreNode = Assert.IsType<CueFloatValue>(scoreField.Value);
        Assert.Equal(95.5, scoreNode.ConcreteValue);
    }
}
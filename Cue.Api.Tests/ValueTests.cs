namespace Cuelang.Cue.Tests;

public sealed class ValueTests
{
    [Fact]
    public void KindAndIncompleteKindMatchNativeKinds()
    {
        // if (!LibcueAvailability.IsAvailable) return;

        using var ctx = new CueContext();
        using var n = ctx.Compile("null");
        using var b = ctx.Compile("bool");
        using var i = ctx.Compile("1");
        using var f = ctx.Compile("1.2");
        using var s = ctx.Compile("\"hello\"");

        Assert.Equal(Kind.Null, n.Kind());
        Assert.Equal(Kind.Bool, b.IncompleteKind());
        Assert.Equal(Kind.Int, i.Kind());
        Assert.Equal(Kind.Float, f.Kind());
        Assert.Equal(Kind.String, s.Kind());
    }

    [Fact]
    public void LookupAndUnifyWorkForNestedValues()
    {
        // if (!LibcueAvailability.IsAvailable) return;

        using var ctx = new CueContext();
        using var foo = ctx.Compile("a: int\nb: _");
        using var bar = ctx.Compile("a: 42\nb: string\nc: true");
        using var unified = foo.Unify(bar);
        using var a = unified.Lookup("a");
        using var c = unified.Lookup("c");

        Assert.Equal(42, a.GetLong());
        Assert.True(c.GetBoolean());
        Assert.Equal(Kind.String, unified.Lookup("b").IncompleteKind());
    }

    [Fact]
    public void ErrorReportsUnificationFailure()
    {
        // if (!LibcueAvailability.IsAvailable) return;

        using var ctx = new CueContext();
        using var foo = ctx.Compile("x: 1");
        using var bar = ctx.Compile("x: 2");
        using var failed = foo.Unify(bar);

        var result = failed.Error();
        Assert.IsType<Result<Value, string>.Err>(result);
    }

    [Fact]
    public void ValidateAndCheckSchemaRespectEvalOptions()
    {
        // if (!LibcueAvailability.IsAvailable) return;

        using var ctx = new CueContext();
        using var concrete = ctx.Compile("1");
        using var schema = ctx.Compile("bool");
        using var value = ctx.ToValue(true);

        concrete.Validate();
        value.CheckSchema(schema, new EvalOption.Final());

        using var nonConcrete = ctx.Compile("int");
        Assert.Throws<CueError>(() => nonConcrete.Validate(new EvalOption.Concrete(true)));
    }

    [Fact]
    public void DefaultValueAndJsonAndBytesRoundTrip()
    {
        // if (!LibcueAvailability.IsAvailable) return;

        using var ctx = new CueContext();
        using var withDefault = ctx.Compile("int | *1");
        using var bytesValue = ctx.Compile("'\\xde\\xad\\xbe\\xef'");
        using var jsonValue = ctx.Compile("a: b: c: 42");

        var defaultValue = withDefault.DefaultValue();

        Assert.NotNull(defaultValue);
        Assert.Equal(1, defaultValue.GetLong());
        Assert.Equal(new byte[] { 0xde, 0xad, 0xbe, 0xef }, bytesValue.GetBytes());
        Assert.Equal("{\"a\":{\"b\":{\"c\":42}}}", jsonValue.GetJson());
    }

    [Fact]
    public void FieldsAndListExposeNativeValueTraversal()
    {
        using var ctx = new CueContext();
        using var root = ctx.Compile("""
            x: 1
            y: true
            nested: {
                list: [10, 20]
            }
            """);
        using var nested = root.Lookup("nested");
        using var listValue = nested.Lookup("list");

        var fields = root.Fields();
        var items = listValue.List();

        try
        {
            Assert.Equal(3, fields.Length);
            Assert.Contains(fields, value => value.Kind() == Kind.Int && value.GetLong() == 1);
            Assert.Contains(fields, value => value.Kind() == Kind.Bool && value.GetBoolean());
            Assert.Contains(fields, value => value.Kind() == Kind.Struct);

            Assert.Equal(2, items.Length);
            Assert.Equal(10, items[0].GetLong());
            Assert.Equal(20, items[1].GetLong());
        }
        finally
        {
            foreach (var field in fields)
            {
                field.Dispose();
            }

            foreach (var item in items)
            {
                item.Dispose();
            }
        }
    }

    [Fact]
    public void LookupNavigatesPropertiesAndListElements()
    {
        using var ctx = new CueContext();
        using var root = ctx.Compile("""
            person: {
                name: "Jane"
                scores: [10, 20, 30]
            }
            """);
        using var person = root.Lookup("person");
        using var name = person.Lookup("name");
        using var secondScore = person.Lookup("scores[1]");

        Assert.Equal("Jane", name.GetString());
        Assert.Equal(20, secondScore.GetLong());
        Assert.Throws<CueError>(() =>
        {
            using var _ = person.Lookup("missing");
        });
        Assert.Throws<CueError>(() =>
        {
            using var _ = person.Lookup("scores[99]");
        });
    }

    [Fact]
    public void PathReturnsExpectedLocationForNestedAndListValues()
    {
        using var ctx = new CueContext();
        using var root = ctx.Compile("""
            person: {
                name: "Jane"
                scores: [10, 20, 30]
            }
            """);
        using var person = root.Lookup("person");
        using var name = person.Lookup("name");
        using var scores = person.Lookup("scores");
        using var secondScore = person.Lookup("scores[1]");

        Assert.Equal("person", person.Path());
        Assert.Equal("person.name", name.Path());
        Assert.Equal("person.scores", scores.Path());
        Assert.Equal("person.scores[1]", secondScore.Path());
    }

    [Fact]
    public void LookupAnyIndexReturnsListElementConstraint()
    {
        using var ctx = new CueContext();
        // A list schema where every element must be an int
        using var listSchema = ctx.Compile("[...int]");
        using var elemConstraint = listSchema.LookupAnyIndex();

        Assert.Equal(Kind.Int, elemConstraint.IncompleteKind());
    }

    [Fact]
    public void LookupAnyIndexThrowsWhenNoIndexConstraintExists()
    {
        using var ctx = new CueContext();
        // A plain struct has no list element constraint
        using var structValue = ctx.Compile("a: 1");

        Assert.Throws<CueError>(() =>
        {
            using var _ = structValue.LookupAnyIndex();
        });
    }

    [Fact]
    public void LookupAnyStringReturnsStructValueConstraint()
    {
        using var ctx = new CueContext();
        // A struct schema where every field value must be a string
        using var mapSchema = ctx.Compile("[string]: string");
        using var valueConstraint = mapSchema.LookupAnyString();

        Assert.Equal(Kind.String, valueConstraint.IncompleteKind());
    }

    [Fact]
    public void LookupAnyStringThrowsWhenNoStringConstraintExists()
    {
        using var ctx = new CueContext();
        // A plain list has no string pattern constraint
        using var listValue = ctx.Compile("[1, 2, 3]");

        Assert.Throws<CueError>(() =>
        {
            using var _ = listValue.LookupAnyString();
        });
    }
}

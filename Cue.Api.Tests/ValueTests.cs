namespace Cuelang.Cue.Tests;

public sealed class ValueTests
{
    [Fact]
    public void KindAndIncompleteKindMatchNativeKinds()
    {
        if (!LibcueAvailability.IsAvailable) return;

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
        if (!LibcueAvailability.IsAvailable) return;

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
        if (!LibcueAvailability.IsAvailable) return;

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
        if (!LibcueAvailability.IsAvailable) return;

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
        if (!LibcueAvailability.IsAvailable) return;

        using var ctx = new CueContext();
        using var withDefault = ctx.Compile("int | *1");
        using var bytesValue = ctx.Compile("'\\xde\\xad\\xbe\\xef'");
        using var jsonValue = ctx.Compile("a: b: c: 42");

        var defaultValue = withDefault.DefaultValue();

        Assert.NotNull(defaultValue);
        Assert.Equal(1, defaultValue!.GetLong());
        Assert.Equal(new byte[] { 0xde, 0xad, 0xbe, 0xef }, bytesValue.GetBytes());
        Assert.Equal("{\"a\":{\"b\":{\"c\":42}}}", jsonValue.GetJson());
    }
}


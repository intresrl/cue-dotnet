namespace Cuelang.Cue.Tests;

public sealed class AttributeTests
{
    [Fact]
    public void AttributeMetadataAndArgumentsAreDecoded()
    {
        // if (!LibcueAvailability.IsAvailable) return;

        using var ctx = new CueContext();
        using var value = ctx.Compile("""
            @foo()
            @bar(foo, bar, baz=qux)

            x: int @baz(1, foo, bar=baz)
            """);

        var rootAttributes = value.Attributes();
        Assert.Equal(2, rootAttributes.Length);
        Assert.Equal("foo", rootAttributes[0].Name());
        Assert.Equal("", rootAttributes[0].Value());
        Assert.Equal(new Attribute.Arg.Value(""), rootAttributes[0].GetArg(0));

        Assert.Equal("bar", rootAttributes[1].Name());
        Assert.Equal(3, rootAttributes[1].ArgCount());
        Assert.Equal(new Attribute.Arg.Value("foo"), rootAttributes[1].GetArg(0));
        Assert.Equal(new Attribute.Arg.KeyValue("baz", "qux"), rootAttributes[1].GetArg(2));

        using var x = value.Lookup("x");
        var fieldAttributes = x.Attributes();
        Assert.Single(fieldAttributes);
        Assert.Equal(new Attribute.Arg.KeyValue("bar", "baz"), fieldAttributes[0].GetArg(2));
    }

    [Fact]
    public void AttributeKindFilteringMatchesDeclarationsAndFields()
    {
        // if (!LibcueAvailability.IsAvailable) return;

        using var ctx = new CueContext();
        using var value = ctx.Compile("""
            x : {
                @foo()
                @bar(baz)

                y: int @qux(quux)
            }
            """);

        using var x = value.Lookup("x");
        using var y = x.Lookup("y");

        Assert.Equal(2, x.Attributes(AttributeKind.Declaration).Length);
        Assert.Empty(x.Attributes(AttributeKind.Field));
        Assert.Empty(y.Attributes(AttributeKind.Declaration));
        Assert.Single(y.Attributes(AttributeKind.Field));
    }
}


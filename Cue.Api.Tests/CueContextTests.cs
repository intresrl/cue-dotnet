namespace Cuelang.Cue.Tests;

public sealed class CueContextTests
{
    [Fact]
    public void TopAndBottomExposeExpectedKinds()
    {
        // if (!LibcueAvailability.IsAvailable) return;

        using var ctx = new CueContext();
        using var top = ctx.Top();
        using var bottom = ctx.Bottom();

        Assert.Equal(Kind.Top, top.IncompleteKind());
        Assert.Equal(Kind.Bottom, bottom.IncompleteKind());
    }

    [Fact]
    public void CompileSupportsTextAndBytesWithBuildOptions()
    {
        // if (!LibcueAvailability.IsAvailable) return;

        using var ctx = new CueContext();

        using var fromString = ctx.Compile(
            "int",
            new BuildOption.FileName("empty.cue"),
            new BuildOption.ImportPath("example.com/foo/bar"),
            new BuildOption.InferBuiltins(true));

        using var fromBytes = ctx.Compile(
            "int"u8.ToArray(),
            new BuildOption.FileName("empty.cue"),
            new BuildOption.ImportPath("example.com/foo/bar"));

        Assert.Equal(Kind.Int, fromString.IncompleteKind());
        Assert.Equal(Kind.Int, fromBytes.IncompleteKind());
    }

    [Fact]
    public void CompileBottomThrowsCueError()
    {
        // if (!LibcueAvailability.IsAvailable) return;

        using var ctx = new CueContext();
        Assert.Throws<CueError>(() => ctx.Compile("_|_"));
    }

    [Fact]
    public void ToValueOverloadsRoundTrip()
    {
        // if (!LibcueAvailability.IsAvailable) return;

        using var ctx = new CueContext();
        using var longValue = ctx.ToValue(-1);
        using var boolValue = ctx.ToValue(true);
        using var doubleValue = ctx.ToValue(0.123);
        using var stringValue = ctx.ToValue("hello");
        using var bytesValue = ctx.ToValue(new byte[] { 1, 2, 3, 4, 5 });
        using var unsignedValue = ctx.ToValueAsUnsigned(0xcafebabe);

        Assert.Equal(-1, longValue.GetLong());
        Assert.True(boolValue.GetBoolean());
        Assert.Equal(0.123, doubleValue.GetDouble(), 3);
        Assert.Equal("hello", stringValue.GetString());
        Assert.Equal(new byte[] { 1, 2, 3, 4, 5 }, bytesValue.GetBytes());
        Assert.Equal(0xcafebabeUL, unsignedValue.GetLongAsUnsigned());
    }

    [Fact]
    public void ToValueSupportsListFromExistingValues()
    {
        using var ctx = new CueContext();
        using var one = ctx.ToValue(1);
        using var two = ctx.ToValue(2);
        using var list = ctx.ToValue(one, two);

        var values = list.List();
        try
        {
            Assert.Equal(2, values.Length);
            Assert.Equal(1, values[0].GetLong());
            Assert.Equal(2, values[1].GetLong());
        }
        finally
        {
            foreach (var value in values)
            {
                value.Dispose();
            }
        }
    }
}


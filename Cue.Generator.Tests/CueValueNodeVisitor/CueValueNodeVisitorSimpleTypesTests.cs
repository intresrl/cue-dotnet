using FsCheck;
using FsCheck.Fluent;
using System.Numerics;
using ExtendedNumerics;

namespace Cue.Generator.Tests.CueValueNodeVisitor;

public sealed class CueValueNodeVisitorSimpleTypesTests
{
    private static Kind GetKind(CueValueNode node)
    {
        return node switch
        {
            CueBottomValue => Kind.Bottom,
            CueTopValue => Kind.Top,
            CueNullValue => Kind.Null,
            CueBoolValue => Kind.Bool,
            CueIntValue => Kind.Int,
            CueFloatValue => Kind.Float,
            CueStringValue => Kind.String,
            CueBytesValue => Kind.Bytes,
            CueNumberValue => Kind.Number,
            _ => throw new ArgumentOutOfRangeException(nameof(node), node, null)
        };
    }

    private static object? GetConcreteValue(CueValueNode node)
    {
        return node switch
        {
            CueBoolValue b => b.ConcreteValue,
            CueIntValue i => i.ConcreteValue,
            CueFloatValue f => f.ConcreteValue,
            CueStringValue s => s.ConcreteValue,
            CueBytesValue b => b.ConcreteValue,
            _ => null
        };
    }

    [Theory]
    [InlineData("false", Kind.Bool, false)]
    [InlineData("bool", Kind.Bool, null)]
    [InlineData("1", Kind.Int, 1L)]
    [InlineData("int | *1", Kind.Int, null)] // int with default
    [InlineData("1 | 2 | 3", Kind.Int, null)] // int with accepted value enumeration
    [InlineData("int", Kind.Int, null)]
    [InlineData("1.2", Kind.Float, 1.2d)]
    [InlineData("float", Kind.Float, null)]
    [InlineData("\"hello\"", Kind.String, "hello")]
    [InlineData("string", Kind.String, null)]
    [InlineData(@"'\xde\xad\xbe\xef'", Kind.Bytes, new byte[] { 222, 173, 190, 239 })]
    [InlineData("bytes", Kind.Bytes, null)]
    [InlineData("number", Kind.Number, null)] // numbers are only generic
    [InlineData("1 | 5.5", Kind.Number, null)] // int + float = number
    [InlineData(">0 & <100", Kind.Number, null)] // constrained number
    [InlineData("null", Kind.Null, null)]
    [InlineData("_", Kind.Top, null)]
    public void VisitSimpleValuesInRoot(string cueSource, Kind kind, object? concrete)
    {
        using var ctx = new CueContext();
        using var value = ctx.Compile(cueSource, new BuildOption.InferBuiltins(true));
        var node = CueValueVisitor.ForTests(value);

        Assert.NotNull(node);
        Assert.Equal(kind, GetKind(node));
        AssertConcreteValueEqual(concrete, GetConcreteValue(node));
        Assert.Equal("", node.Path);
    }

    // Concrete int/float values are now represented internally as BigInteger/BigDecimal so they
    // are captured exactly, but InlineData can't carry those types as constant attribute
    // arguments - so InlineData still uses long/double, and we convert on the way out for
    // comparison.
    private static void AssertConcreteValueEqual(object? expected, object? actual)
    {
        switch (expected, actual)
        {
            case (long l, BigInteger bi):
                Assert.Equal(l, (long)bi);
                break;
            case (double d, BigDecimal bd):
                Assert.Equal(d, (double)bd);
                break;
            default:
                Assert.Equal(expected, actual);
                break;
        }
    }

    [Fact]
    private void SourceRoundTripsToIdenticalTree()
    {
        using var ctx = new CueContext();

        var property = Prop.ForAll(
            CueValueNodeArbitrary.Arbitrary,
            node =>
            {
                var source = node.Source();
                using var value = ctx.Compile(source, new BuildOption.InferBuiltins(true));
                Assert.Equal(node, CueValueVisitor.ForTests(value), new Roslyn.CueValueNodeComparer());
            });
        
        var config = Config.Quick
            .WithMaxTest(1_000_000)
            .WithEvery((n, test) => $"Test {n}: {test}")
            .WithEveryShrink(config => $"Shrink");
        
        Check.One(config, property);
    }
}
using System.Text.Json;
using FsCheck;
using FsCheck.Fluent;

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
    [InlineData("int | bool | string", Kind.Top, null)] // when type is heterogeneous, kind is top 
    [InlineData("int | null", Kind.Top,
        null)] // nullable types are top // TODO: handle tops caused by this as nullable types
    public void VisitSimpleValuesInRoot(string cueSource, Kind kind, object? concrete)
    {
        using var ctx = new CueContext();
        using var value = ctx.Compile(cueSource, new BuildOption.InferBuiltins(true));
        var node = value.ToCueValueNode();

        Assert.NotNull(node);
        Assert.Equal(kind, GetKind(node));
        Assert.Equal(concrete, GetConcreteValue(node));
        Assert.Equal("", node.Path);
    }

    [Fact]
    private void SourceRoundTripsToIdenticalTree()
    {
        using var ctx = new CueContext();

        Prop.ForAll(
                CueArbitrary.Arbitrary,
                node =>
                {
                    using var value = ctx.Compile(node.Source(), new BuildOption.InferBuiltins(true));
                    var roundTripped = value.ToCueValueNode();
                    return TreesIdentical(node, roundTripped);
                })
            .QuickCheckThrowOnFailure();
    }

    private static bool TreesIdentical(CueValueNode left, CueValueNode right)
    {
        if (left.GetType() != right.GetType())
            return false;

        if (left.Path != right.Path)
            return false;

        return (left, right) switch
        {
            (CueBottomValue, CueBottomValue) => true,
            (CueTopValue, CueTopValue) => true,
            (CueNullValue, CueNullValue) => true,
            (CueNumberValue, CueNumberValue) => true,
            (CueBoolValue x, CueBoolValue y) => x.ConcreteValue == y.ConcreteValue,
            (CueIntValue x, CueIntValue y) => x.ConcreteValue == y.ConcreteValue,
            (CueFloatValue x, CueFloatValue y) => x.ConcreteValue.Equals(y.ConcreteValue),
            (CueStringValue x, CueStringValue y) => x.ConcreteValue == y.ConcreteValue,
            (CueBytesValue { ConcreteValue: var a }, CueBytesValue { ConcreteValue: var b }) => (a, b) switch
            {
                (null, null) => true,
                (not null, not null) => a.SequenceEqual(b),
                _ => false
            },

            (CueStructValue x, CueStructValue y) =>
                x.Fields.Count == y.Fields.Count &&
                x.Fields.Zip(y.Fields).All(pair =>
                    pair.First.Name == pair.Second.Name &&
                    pair.First.Optional == pair.Second.Optional &&
                    TreesIdentical(pair.First.Value, pair.Second.Value)),

            (CueListValue x, CueListValue y) =>
                TreesIdentical(x.ElementType, y.ElementType),

            (CueDisjunction x, CueDisjunction y) =>
                x.Branches.Count == y.Branches.Count &&
                x.Branches.Zip(y.Branches).All(pair =>
                    TreesIdentical(pair.First, pair.Second)),

            _ => false
        };
    }
}

public static class CueArbitrary
{
    public static Arbitrary<CueValueNode> Arbitrary =>
        Arb.From(
            Gen(
                ArbMap.Default.ArbFor<bool>(),
                ArbMap.Default.ArbFor<byte>(),
                ArbMap.Default.ArbFor<int>(),
                ArbMap.Default.ArbFor<double>(),
                ArbMap.Default.ArbFor<string>()
            )
        );

    private static Gen<CueValueNode> Gen(
        Arbitrary<bool> bools,
        Arbitrary<byte> bytes,
        Arbitrary<int> ints,
        Arbitrary<double> doubles,
        Arbitrary<string> strings)
    {
        return FsCheck.Fluent.Gen.OneOf(
            FsCheck.Fluent.Gen.Elements<CueValueNode>(
                new CueBottomValue(""),
                new CueTopValue(""),
                new CueNullValue(""),
                new CueNumberValue(""),
                new CueBoolValue(""),
                new CueBytesValue(""),
                new CueFloatValue(""),
                new CueIntValue(""),
                new CueStringValue("")),
            bools.Generator.Select(CueValueNode (b) => new CueBoolValue("", b)),
            ints.Generator
                .Select(CueValueNode (n) => new CueIntValue("", n)),
            doubles.Generator
                .Select(CueValueNode (d) => new CueFloatValue("", d)),
            strings.Generator
                .Select(CueValueNode (s) => new CueStringValue("", s)),
            bytes.Generator
                .ArrayOf()
                .Select(CueValueNode (bs) => new CueBytesValue("", bs))
        );
    }

    public static string Source(this CueValueNode node)
    {
        return node switch
        {
            CueBottomValue => "_|_",
            CueTopValue => "_",
            CueNullValue => "null",
            CueNumberValue => "number",

            CueBoolValue { ConcreteValue: var v } => v?.ToString() ?? "bool",
            CueBytesValue { ConcreteValue: var v } => v?.ToString() ?? "bytes",
            CueFloatValue { ConcreteValue: var v } => v?.ToString() ?? "float",
            CueIntValue { ConcreteValue: var v } => v?.ToString() ?? "int",
            CueStringValue { ConcreteValue: var v } => v != null
                ? JsonSerializer.Serialize(v)
                : "string", // CUE string literals are a superset of JSON

            CueStructValue { Fields: var f } => string.Join("\n", [
                "{",
                .. f.Select(e =>
                {
                    var name = JsonSerializer.Serialize(e.Name);
                    var delimiter = e.Optional ? "?:" : ":";
                    var value = e.Value.Source();

                    return $"  {name}{delimiter}{value}";
                }),
                "}"
            ]),

            CueListValue { ElementType: var v } => $"""
                                                    [
                                                      ... {v.Source()}
                                                    ]
                                                    """,

            CueDisjunction { Branches: var bs } => string.Join(" | ", bs),


            _ => throw new ArgumentOutOfRangeException(nameof(node))
        };
    }
}
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using FsCheck;
using FsCheck.Fluent;

namespace Cue.Generator.Tests.CueValueNodeVisitor;

public static class CueValueNodeArbitrary
{
    private static readonly Arbitrary<bool> Bools = ArbMap.Default.ArbFor<bool>();
    private static readonly Arbitrary<byte> Bytes = ArbMap.Default.ArbFor<byte>();
    private static readonly Arbitrary<int> Ints = ArbMap.Default.ArbFor<int>();
    private static readonly Arbitrary<double> Doubles = FiniteDoubles();
    private static readonly Arbitrary<string> Strings = StringsWithoutNull();

    public static Arbitrary<CueValueNode> Arbitrary => Arb.From(Generator);

    private static readonly Gen<CueValueNode> Generator = Gen.Sized(size =>
    {
        var simple =
            Gen.Frequency(
                (1, Gen.Constant<CueValueNode>(new CueTopValue(""))),
                (1, Gen.Constant<CueValueNode>(new CueNullValue(""))),
                (1, Gen.Constant<CueValueNode>(new CueNumberValue(""))),
                (1, Gen.Constant<CueValueNode>(new CueBoolValue(""))),
                (1, Gen.Constant<CueValueNode>(new CueBytesValue(""))),
                (1, Gen.Constant<CueValueNode>(new CueFloatValue(""))),
                (1, Gen.Constant<CueValueNode>(new CueIntValue(""))),
                (1, Gen.Constant<CueValueNode>(new CueStringValue(""))),
                (5, Bools.Generator.Select(CueValueNode (b) => new CueBoolValue("", b))),
                (5, Ints.Generator.Select(CueValueNode (n) => new CueIntValue("", n))),
                (5, Doubles.Generator.Select(CueValueNode (d) => new CueFloatValue("", d))),
                (10, Strings.Generator.Select(CueValueNode (s) => new CueStringValue("", s)))
            );

        //,bytes.Generator
        //    .ArrayOf()
        //    .Select(CueValueNode (bs) => new CueBytesValue("", bs))
        // Don't generate recursive structures at size 0.
        return size <= 0
            ? simple
            : Gen.Frequency(
                (20, simple),
                (15, Gen.Frequency(
                    (3, StructGen(size - 1)),
                    (2, ListGen(size - 1)))));
    });

    private static Gen<CueValueNode> StructGen(int size)
    {
        return Gen
            .Choose(0, Math.Min(size, 4))
            .SelectMany(fieldCount => Strings.Generator
                .SelectMany(_ => Generator, (name, value) => new CueStructField(name, value))
                .ArrayOf(fieldCount))
            .Select(CueValueNode (fields) => new CueStructValue("", fields));
    }

    private static Gen<CueValueNode> ListGen(int size)
    {
        return Gen
            .Choose(0, Math.Min(size, 4))
            .SelectMany(_ => Generator)
            .Select(CueValueNode (elementType) => new CueListValue("", elementType, []));
    }

    // TODO: NUL character in strings bugs API because strings are CStrings
    private static Arbitrary<string> StringsWithoutNull()
    {
        return ArbMap.Default.ArbFor<string>()
            .Filter(s => !s.Contains('\0'));
    }

    // cue does not support infinities and NaN as values for float. float is arbitrary precision
    private static Arbitrary<double> FiniteDoubles()
    {
        return ArbMap.Default.ArbFor<double>()
            .Filter(double.IsFinite);
    }

    private static string AsJson(object? value, string typeName)
    {
        return value is null
            ? typeName
            : JsonSerializer.Serialize(value, Options);
    }

    private static readonly JsonSerializerOptions Options = new()
    {
        Encoder = JavaScriptEncoder.Default,
        NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals
    };

    public static string Source(this CueValueNode node)
    {
        return node switch
        {
            CueBottomValue => "_|_",
            CueTopValue => "_",
            CueNullValue => "null",
            CueNumberValue => "number",

            CueBoolValue { ConcreteValue: var v } => AsJson(v, "bool"),
            CueBytesValue { ConcreteValue: var v } => AsJson(v != null ? Convert.ToBase64String(v) : null, "bytes"),
            CueFloatValue { ConcreteValue: var v } => AsJson(v, "float"),
            CueIntValue { ConcreteValue: var v } => AsJson(v, "int"),
            CueStringValue { ConcreteValue: var v } => AsJson(v, "string"),

            CueStructValue { Fields: var f } => string.Join("\n", [
                "{",
                .. f.Select(e =>
                {
                    var name = JsonSerializer.Serialize(e.Name, Options);
                    var delimiter = e.Optional ? "?:" : ":";
                    var value = e.Value.Source();

                    return $"  {name}{delimiter}{value}";
                }),
                "}"
            ]),

            CueListValue { AnyIndexElement: var v } => $"""
                                                    [
                                                      ... ({v.Source()})
                                                    ]
                                                    """,

            CueDisjunction { Branches: var bs } => string.Join(" | ", bs),


            _ => throw new ArgumentOutOfRangeException(nameof(node))
        };
    }
}
using System.Numerics;

public readonly record struct BoundRegexMatch(string Value)
{
    public static bool IsValid(string value) => value == "^x";
}

public readonly record struct BoundRegexNotMatch(string Value)
{
    public static bool IsValid(string value) => value == "^x";
}

public readonly record struct Default(byte Value)
{
    public static bool IsValid(byte value) => value == 1L || value == 2L;
}

public readonly record struct Unify(BigInteger Value)
{
    public static bool IsValid(BigInteger value) => true && value >= 0L;
}

public class DateTimeMessage
{
    public string Type { get; init; }
    public S Format { get; init; }
    public string Timezone { get; init; }
}

public class Obj
{
    public long A { get; init; }
}

public class TextMessage
{
    public string Type { get; init; }
    public Selector MaxLength { get; init; }
    public string Pattern { get; init; }
}
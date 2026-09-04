using System.Numerics;

public class ConvertedPerson
{
    public ConvertedString Name { get; init; }
    public long Age { get; init; }
}

public class ConvertedSettings
{
    public bool Enabled { get; init; }
    public string Mode { get; init; }
}

public readonly record struct PlainBool(bool Value)
{
    public static bool IsValid(bool value) => value == true;
}

public readonly record struct PlainInt(byte Value)
{
    public static bool IsValid(byte value) => value == 123L;
}

public readonly record struct PlainString(string Value)
{
    public static bool IsValid(string value) => value == "this top-level property should be ignored";
}

public class PlainStruct
{
    public string Name { get; init; }
}
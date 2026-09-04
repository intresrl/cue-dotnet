using System;
using System.Collections.Generic;
using System.Numerics;
using ExtendedNumerics;

public readonly struct ConvertedList(List<ConvertedString> value)
{
    public List<ConvertedString> Value { get; } = value;

    public implicit operator ConvertedList(List<ConvertedString> value) => new(value);
}

public readonly struct PlainList((string, string) value)
{
    public (string, string) Value { get; } = value;

    public implicit operator PlainList((string, string) value) => new(value);
}

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

public readonly record struct ConvertedString(string Value)
{
    public static bool IsValid(string value) => true;
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
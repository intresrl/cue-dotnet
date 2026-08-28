using System;
using System.Collections.Generic;

public readonly struct ConvertedList(List<ConvertedString> value)
{
    public List<ConvertedString> Value { get; } = value;

    public implicit operator ConvertedList(List<ConvertedString> value) => new(value);
}

public readonly struct ConvertedString(string value)
{
    public string Value { get; } = value;

    public implicit operator ConvertedString(string value) => new(value);
}

public readonly struct PlainBool(bool value)
{
    public bool Value { get; } = value;

    public implicit operator PlainBool(bool value) => new(value);
}

public readonly struct PlainInt(long value)
{
    public long Value { get; } = value;

    public implicit operator PlainInt(long value) => new(value);
}

public readonly struct PlainList((string, string) value)
{
    public (string, string) Value { get; } = value;

    public implicit operator PlainList((string, string) value) => new(value);
}

public readonly struct PlainString(string value)
{
    public string Value { get; } = value;

    public implicit operator PlainString(string value) => new(value);
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

public class PlainStruct
{
    public string Name { get; init; }
}
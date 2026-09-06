using System;
using System.Collections.Generic;
using System.Numerics;
using ExtendedNumerics;
using System.Text.RegularExpressions;

public readonly struct ConvertedList(List<ConvertedString> value)
{
    public List<ConvertedString> Value { get; } = value;

    public implicit operator ConvertedList(List<ConvertedString> value) => new(value);
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
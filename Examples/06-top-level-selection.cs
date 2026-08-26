using System;
using System.Collections.Generic;

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
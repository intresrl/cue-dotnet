using System;
using System.Collections.Generic;
using System.Numerics;
using ExtendedNumerics;
using System.Text.RegularExpressions;

public readonly struct Coordinates((decimal, decimal, decimal) value)
{
    public (decimal, decimal, decimal) Value { get; } = value;

    public static implicit operator Coordinates((decimal, decimal, decimal) value) => new(value);
}

public readonly struct Numbers(List<long> value)
{
    public List<long> Value { get; } = value;

    public static implicit operator Numbers(List<long> value) => new(value);
}

public class Order
{
    public required string Id { get; init; }
    public required List<OrderitemsAny> Items { get; init; }
}

public class OrderitemsAny
{
    public required string Sku { get; init; }
    public required long Quantity { get; init; }
}

public class Profile
{
    public required string DisplayName { get; init; }
    public required Profilesettings Settings { get; init; }
    public List<string> Tags { get; init; }
}

public class Profilesettings
{
    public required string Theme { get; init; }
    public required bool Notifications { get; init; }
}
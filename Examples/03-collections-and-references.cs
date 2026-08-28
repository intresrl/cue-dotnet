using System;
using System.Collections.Generic;
using System.Collections.Generic;

public sealed class CueList<TConcrete, TAnyIndex>
{
    public required TConcrete Concrete { get; init; }
    public List<TAnyIndex> AnyIndex { get; init; } = [];
}

public readonly struct FixedLiteralTuple((string, long, bool) value)
{
    public (string, long, bool) Value { get; } = value;

    public implicit operator FixedLiteralTuple((string, long, bool) value) => new(value);
}

public readonly struct FixedLiteralTupleWithTail(CueList<(string, long, bool), Scores> value)
{
    public CueList<(string, long, bool), Scores> Value { get; } = value;

    public implicit operator FixedLiteralTupleWithTail(CueList<(string, long, bool), Scores> value) => new(value);
}

public readonly struct FixedPrimitiveTuple((string, long, bool) value)
{
    public (string, long, bool) Value { get; } = value;

    public implicit operator FixedPrimitiveTuple((string, long, bool) value) => new(value);
}

public readonly struct FixedStructTuple((FixedStructTuple0, FixedStructTuple1) value)
{
    public (FixedStructTuple0, FixedStructTuple1) Value { get; } = value;

    public implicit operator FixedStructTuple((FixedStructTuple0, FixedStructTuple1) value) => new(value);
}

public readonly struct MixedTuple((string, long, string? ) value)
{
    public (string, long, string? ) Value { get; } = value;

    public implicit operator MixedTuple((string, long, string? ) value) => new(value);
}

public readonly struct Role(string value)
{
    public string Value { get; } = value;

    public implicit operator Role(string value) => new(value);
}

public readonly struct StringList(List<string> value)
{
    public List<string> Value { get; } = value;

    public implicit operator StringList(List<string> value) => new(value);
}

public class CollectionsAndReferencesExample
{
    public Order Order { get; init; }
    public User User { get; init; }
    public InlineOrder InlineOrder { get; init; }
    public Scores Scores { get; init; }
    public Organization Organization { get; init; }
    public StringList Strings { get; init; }
    public StringList Items { get; init; }
}

public class Department
{
    public string Name { get; init; }
    public List<User> Members { get; init; }
}

public class FixedStructTuple0
{
    public string Id { get; init; }
}

public class FixedStructTuple1
{
    public long Count { get; init; }
}

public class InlineOrder
{
    public List<Item> Items { get; init; }
}

public class Item
{
    public string Sku { get; init; }
    public long Quantity { get; init; }
}

public class Order
{
    public string Id { get; init; }
    public List<Item> Items { get; init; }
}

public class Organization
{
    public string Name { get; init; }
    public List<Department> Departments { get; init; }
}

public class Scores
{
}

public class User
{
    public string Id { get; init; }
    public List<Role> Roles { get; init; }
}
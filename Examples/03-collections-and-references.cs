using System;
using System.Collections.Generic;
using System.Numerics;
using ExtendedNumerics;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace Examples.collections_and_references
{
    public sealed class CueList<TConcrete, TAnyIndex>
    {
        public required TConcrete Concrete { get; init; }
        public List<TAnyIndex> AnyIndex { get; init; } = [];
    }

    public readonly struct FixedLiteralTuple((string, long, bool) value)
    {
        public (string, long, bool) Value { get; } = value;

        public static implicit operator FixedLiteralTuple((string, long, bool) value) => new(value);
    }

    public readonly struct FixedLiteralTupleWithTail(CueList<(string, long, bool), Scores> value)
    {
        public CueList<(string, long, bool), Scores> Value { get; } = value;

        public static implicit operator FixedLiteralTupleWithTail(CueList<(string, long, bool), Scores> value) => new(value);
    }

    public readonly struct FixedPrimitiveTuple((string, long, bool) value)
    {
        public (string, long, bool) Value { get; } = value;

        public static implicit operator FixedPrimitiveTuple((string, long, bool) value) => new(value);
    }

    public readonly struct FixedStructTuple((FixedStructTuple0, FixedStructTuple1) value)
    {
        public (FixedStructTuple0, FixedStructTuple1) Value { get; } = value;

        public static implicit operator FixedStructTuple((FixedStructTuple0, FixedStructTuple1) value) => new(value);
    }

    public readonly struct MixedTuple((string, long, string? ) value)
    {
        public (string, long, string? ) Value { get; } = value;

        public static implicit operator MixedTuple((string, long, string? ) value) => new(value);
    }

    public readonly struct StringList(List<string> value)
    {
        public List<string> Value { get; } = value;

        public static implicit operator StringList(List<string> value) => new(value);
    }

    public class CollectionsAndReferencesExample
    {
        public required Order Order { get; init; }
        public required User User { get; init; }
        public required InlineOrder InlineOrder { get; init; }
        public required Scores Scores { get; init; }
        public required Organization Organization { get; init; }
        public required StringList Strings { get; init; }
        public required StringList Items { get; init; }
    }

    public class Department
    {
        public required string Name { get; init; }
        public required List<User> Members { get; init; }
    }

    public class FixedStructTuple0
    {
        public required string Id { get; init; }
    }

    public class FixedStructTuple1
    {
        public required long Count { get; init; }
    }

    public class InlineOrder
    {
        public required List<Item> Items { get; init; }
    }

    public class Item
    {
        public required string Sku { get; init; }
        public required long Quantity { get; init; }
    }

    public class Order
    {
        public required string Id { get; init; }
        public required List<Item> Items { get; init; }
    }

    public class Organization
    {
        public required string Name { get; init; }
        public required List<Department> Departments { get; init; }
    }

    public readonly record struct Role(string Value)
    {
        public static bool IsValid(string value) => value == "admin" || value == "editor" || value == "viewer";
    }

    public class Scores
    {
    }

    public class User
    {
        public required string Id { get; init; }
        public required List<Role> Roles { get; init; }
    }
}
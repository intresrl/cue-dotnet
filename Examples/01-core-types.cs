using System;
using System.Collections.Generic;
using System.Numerics;
using ExtendedNumerics;
using System.Text.RegularExpressions;

namespace Examples.core_types
{
    public readonly record struct AnyBool(bool Value)
    {
        public static bool IsValid(bool value) => true;
    }

    public readonly record struct AnyInt(BigInteger Value)
    {
        public static bool IsValid(BigInteger value) => true;
    }

    public readonly record struct AnyString(string Value)
    {
        public static bool IsValid(string value) => true;
    }

    public readonly record struct BoundedInt(byte Value)
    {
        public static bool IsValid(byte value) => value >= 1 && value <= 10;
    }

    public class ConcreteValues
    {
        public required string Status { get; init; }
        public required long Version { get; init; }
        public required bool Enabled { get; init; }
    }

    public class ConstrainedValues
    {
        public required long Age { get; init; }
        public required decimal Percentage { get; init; }
        public required string Email { get; init; }
    }

    public class CoreTypesExample
    {
        public required PrimitiveTypes Primitives { get; init; }
        public required ConcreteValues Concrete { get; init; }
        public required OptionalAndNullable Fields { get; init; }
        public required ConstrainedValues Constrained { get; init; }
        public required Status Status { get; init; }
        public required Priority Priority { get; init; }
    }

    public readonly record struct EmailString(string Value)
    {
        public static bool IsValid(string value) => Regex.IsMatch(value, "^.+@.+$");
    }

    public readonly record struct IntChoice(byte Value)
    {
        public static bool IsValid(byte value) => value == 1 || value == 2 || value == 3;
    }

    public readonly record struct LiteralBool(bool Value)
    {
        public static bool IsValid(bool value) => value == true;
    }

    public readonly record struct LiteralInt(byte Value)
    {
        public static bool IsValid(byte value) => value == 42;
    }

    public readonly record struct LiteralNumber(BigDecimal Value)
    {
        public static bool IsValid(BigDecimal value) => value == 3.139648M;
    }

    public readonly record struct LiteralString(string Value)
    {
        public static bool IsValid(string value) => value == "hello";
    }

    public readonly record struct NonEmptyString(string Value)
    {
        public static bool IsValid(string value) => value != "";
    }

    public class OptionalAndNullable
    {
        public required string Required { get; init; }
        public string Optional { get; init; }
        public required string? Nullable { get; init; }
        public string? OptionalNullable { get; init; }
    }

    public readonly record struct PositiveInt(BigInteger Value)
    {
        public static bool IsValid(BigInteger value) => value > 0;
    }

    public class PrimitiveTypes
    {
        public required string Text { get; init; }
        public required long Integer { get; init; }
        public required decimal Decimal { get; init; }
        public required bool Enabled { get; init; }
    }

    public readonly record struct Priority(byte Value)
    {
        public static bool IsValid(byte value) => value == 1 || value == 2 || value == 3;
    }

    public readonly record struct Status(string Value)
    {
        public static bool IsValid(string value) => value == "pending" || value == "running" || value == "completed";
    }

    public readonly record struct StringChoice(string Value)
    {
        public static bool IsValid(string value) => value == "alpha" || value == "beta" || value == "gamma";
    }
}
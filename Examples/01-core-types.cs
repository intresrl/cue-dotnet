using System;
using System.Collections.Generic;
using System.Numerics;
using ExtendedNumerics;

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
    public static bool IsValid(byte value) => true && value >= 1 && value <= 10;
}

public class ConcreteValues
{
    public string Status { get; init; }
    public long Version { get; init; }
    public bool Enabled { get; init; }
}

public class ConstrainedValues
{
    public long Age { get; init; }
    public decimal Percentage { get; init; }
    public string Email { get; init; }
}

public class CoreTypesExample
{
    public PrimitiveTypes Primitives { get; init; }
    public ConcreteValues Concrete { get; init; }
    public OptionalAndNullable Fields { get; init; }
    public ConstrainedValues Constrained { get; init; }
    public Status Status { get; init; }
    public Priority Priority { get; init; }
}

public readonly record struct EmailString(string Value)
{
    public static bool IsValid(string value) => true && System.Text.RegularExpressions.Regex.IsMatch(value, "^.+@.+$");
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
    public static bool IsValid(BigDecimal value) => value == BigDecimal.Parse("3.14000000000000000009540979117872439019265584647655487060546875");
}

public readonly record struct LiteralString(string Value)
{
    public static bool IsValid(string value) => value == "hello";
}

public readonly record struct NonEmptyString(string Value)
{
    public static bool IsValid(string value) => true && value != "";
}

public class OptionalAndNullable
{
    public string Required { get; init; }
    public string Optional { get; init; }
    public string? Nullable { get; init; }
    public string? OptionalNullable { get; init; }
}

public readonly record struct PositiveInt(BigInteger Value)
{
    public static bool IsValid(BigInteger value) => true && value > 0;
}

public class PrimitiveTypes
{
    public string Text { get; init; }
    public long Integer { get; init; }
    public decimal Decimal { get; init; }
    public bool Enabled { get; init; }
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
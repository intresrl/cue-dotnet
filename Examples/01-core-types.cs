using System;
using System.Collections.Generic;

public readonly struct AnyBool(bool value)
{
    public bool Value { get; } = value;

    public implicit operator AnyBool(bool value) => new(value);
}

public readonly struct AnyInt(long value)
{
    public long Value { get; } = value;

    public implicit operator AnyInt(long value) => new(value);
}

public readonly struct AnyNumber(decimal value)
{
    public decimal Value { get; } = value;

    public implicit operator AnyNumber(decimal value) => new(value);
}

public readonly struct AnyString(string value)
{
    public string Value { get; } = value;

    public implicit operator AnyString(string value) => new(value);
}

public readonly struct BoundedInt(long value)
{
    public long Value { get; } = value;

    public implicit operator BoundedInt(long value) => new(value);
}

public readonly struct EmailString(string value)
{
    public string Value { get; } = value;

    public implicit operator EmailString(string value) => new(value);
}

public readonly struct LiteralBool(bool value)
{
    public bool Value { get; } = value;

    public implicit operator LiteralBool(bool value) => new(value);
}

public readonly struct LiteralInt(long value)
{
    public long Value { get; } = value;

    public implicit operator LiteralInt(long value) => new(value);
}

public readonly struct LiteralNumber(double value)
{
    public double Value { get; } = value;

    public implicit operator LiteralNumber(double value) => new(value);
}

public readonly struct LiteralString(string value)
{
    public string Value { get; } = value;

    public implicit operator LiteralString(string value) => new(value);
}

public readonly struct NonEmptyString(string value)
{
    public string Value { get; } = value;

    public implicit operator NonEmptyString(string value) => new(value);
}

public readonly struct NullValue(object value)
{
    public object Value { get; } = value;

    public implicit operator NullValue(object value) => new(value);
}

public readonly struct PositiveInt(long value)
{
    public long Value { get; } = value;

    public implicit operator PositiveInt(long value) => new(value);
}

public readonly struct PositiveNumber(decimal value)
{
    public decimal Value { get; } = value;

    public implicit operator PositiveNumber(decimal value) => new(value);
}

public readonly struct Priority(long value)
{
    public long Value { get; } = value;

    public implicit operator Priority(long value) => new(value);
}

public readonly struct Status(string value)
{
    public string Value { get; } = value;

    public implicit operator Status(string value) => new(value);
}

public readonly struct StringChoice(string value)
{
    public string Value { get; } = value;

    public implicit operator StringChoice(string value) => new(value);
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

public class OptionalAndNullable
{
    public string Required { get; init; }
    public string Optional { get; init; }
    public string? Nullable { get; init; }
    public string? OptionalNullable { get; init; }
}

public class PrimitiveTypes
{
    public string Text { get; init; }
    public long Integer { get; init; }
    public decimal Decimal { get; init; }
    public bool Enabled { get; init; }
}
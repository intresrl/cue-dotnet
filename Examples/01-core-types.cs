using System;
using System.Collections.Generic;

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
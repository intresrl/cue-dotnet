using System;
using System.Collections.Generic;

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

public class FixedStructTuple
{
    public string Id { get; init; }
}

public class FixedStructTuple
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
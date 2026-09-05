using System;
using System.Collections.Generic;
using System.Numerics;
using ExtendedNumerics;
using System.Text.RegularExpressions;

public class Address
{
    public string Street { get; init; }
    public string City { get; init; }
    public string Country { get; init; }
}

public class Employee
{
    public string EmployeeNumber { get; init; }
    public string Department { get; init; }
    public string Id { get; init; }
    public string CreatedAt { get; init; }
    public string Name { get; init; }
    public string Email { get; init; }
}

public class Entity
{
    public string Id { get; init; }
    public string CreatedAt { get; init; }
}

public class Person
{
    public string Name { get; init; }
    public Address Address { get; init; }
}

public class Profile
{
    public string DisplayName { get; init; }
    public Profilesettings Settings { get; init; }
}

public class Profilesettings
{
    public string Theme { get; init; }
    public bool Notifications { get; init; }
}

public class StructsAndCompositionExample
{
    public Person Person { get; init; }
    public User User { get; init; }
    public Employee Employee { get; init; }
    public TaggedEntity Tagged { get; init; }
    public Profile Profile { get; init; }
}

public class TaggedEntity
{
    public string Id { get; init; }
    public string CreatedAt { get; init; }
    public List<string> Tags { get; init; }
}

public class User
{
    public string Id { get; init; }
    public string CreatedAt { get; init; }
    public string Name { get; init; }
    public string Email { get; init; }
}
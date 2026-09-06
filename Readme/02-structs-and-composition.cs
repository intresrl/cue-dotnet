using System;
using System.Collections.Generic;
using System.Numerics;
using ExtendedNumerics;
using System.Text.RegularExpressions;

public class Address
{
    public required string Street { get; init; }
    public required string City { get; init; }
    public required string Country { get; init; }
}

public class Employee
{
    public required string Name { get; init; }
    public string Email { get; init; }
    public required string? Notes { get; init; }
    public required string EmployeeNumber { get; init; }
    public required string Department { get; init; }
}

public class Person
{
    public required string Name { get; init; }
    public required Address Address { get; init; }
    public string Email { get; init; }
    public required string? Notes { get; init; }
}
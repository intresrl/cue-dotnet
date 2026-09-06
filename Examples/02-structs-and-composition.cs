using System;
using System.Collections.Generic;
using System.Numerics;
using ExtendedNumerics;
using System.Text.RegularExpressions;

namespace Examples.structs_and_composition
{
    public class Address
    {
        public required string Street { get; init; }
        public required string City { get; init; }
        public required string Country { get; init; }
    }

    public class Employee
    {
        public required string EmployeeNumber { get; init; }
        public required string Department { get; init; }
        public required string Id { get; init; }
        public required string CreatedAt { get; init; }
        public required string Name { get; init; }
        public required string Email { get; init; }
    }

    public class Entity
    {
        public required string Id { get; init; }
        public required string CreatedAt { get; init; }
    }

    public class Person
    {
        public required string Name { get; init; }
        public required Address Address { get; init; }
    }

    public class Profile
    {
        public required string DisplayName { get; init; }
        public required Profilesettings Settings { get; init; }
    }

    public class Profilesettings
    {
        public required string Theme { get; init; }
        public required bool Notifications { get; init; }
    }

    public class StructsAndCompositionExample
    {
        public required Person Person { get; init; }
        public required User User { get; init; }
        public required Employee Employee { get; init; }
        public required TaggedEntity Tagged { get; init; }
        public required Profile Profile { get; init; }
    }

    public class TaggedEntity
    {
        public required string Id { get; init; }
        public required string CreatedAt { get; init; }
        public List<string> Tags { get; init; }
    }

    public class User
    {
        public required string Id { get; init; }
        public required string CreatedAt { get; init; }
        public required string Name { get; init; }
        public required string Email { get; init; }
    }
}
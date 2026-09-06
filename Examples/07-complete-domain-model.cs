using System;
using System.Collections.Generic;
using System.Numerics;
using ExtendedNumerics;
using System.Text.RegularExpressions;

namespace Examples.complete_domain_model
{
    public interface ProductvalueBase
    {
        public record AsPhysicalProduct(PhysicalProduct value) : ProductvalueBase;
        public record AsDigitalProduct(DigitalProduct value) : ProductvalueBase;
        public record Value(ProductvalueBase[] Branches)
        {
            public bool Valid => Branches.Length == 1;
        };
    }

    public class Address
    {
        public required string Street { get; init; }
        public required string City { get; init; }
        public string? Country { get; init; }
    }

    public class BaseEntity
    {
        public required string Id { get; init; }
    }

    public class CompleteDomainModel
    {
        public required Organization Organization { get; init; }
    }

    public class DigitalProduct
    {
        public required string Type { get; init; }
        public required string Sku { get; init; }
        public required string DownloadUrl { get; init; }
    }

    public class Order
    {
        public required User Customer { get; init; }
        public required string Id { get; init; }
        public required List<OrderLine> Lines { get; init; }
    }

    public class OrderLine
    {
        public required Product Product { get; init; }
        public required long Quantity { get; init; }
    }

    public class Organization
    {
        public required string Name { get; init; }
        public required List<User> Users { get; init; }
        public required List<Order> Orders { get; init; }
    }

    public class PhysicalProduct
    {
        public required string Type { get; init; }
        public required string Sku { get; init; }
        public required decimal Weight { get; init; }
    }

    public class Product
    {
        public required ProductvalueBase Value { get; init; }
    }

    public readonly record struct Role(string Value)
    {
        public static bool IsValid(string value) => value == "admin" || value == "editor" || value == "viewer";
    }

    public class User
    {
        public required string Id { get; init; }
        public required string Name { get; init; }
        public required string Email { get; init; }
        public required UserStatus Status { get; init; }
        public required List<Role> Roles { get; init; }
        public Address? Address { get; init; }
    }

    public readonly record struct UserStatus(string Value)
    {
        public static bool IsValid(string value) => value == "active" || value == "disabled";
    }
}
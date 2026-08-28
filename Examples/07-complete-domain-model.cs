using System;
using System.Collections.Generic;

public readonly struct Role(string value)
{
    public string Value { get; } = value;

    public implicit operator Role(string value) => new(value);
}

public readonly struct UserStatus(string value)
{
    public string Value { get; } = value;

    public implicit operator UserStatus(string value) => new(value);
}

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
    public string Street { get; init; }
    public string City { get; init; }
    public string? Country { get; init; }
}

public class BaseEntity
{
    public string Id { get; init; }
}

public class CompleteDomainModel
{
    public Organization Organization { get; init; }
}

public class DigitalProduct
{
    public string Type { get; init; }
    public string Sku { get; init; }
    public string DownloadUrl { get; init; }
}

public class Order
{
    public User Customer { get; init; }
    public string Id { get; init; }
    public List<OrderLine> Lines { get; init; }
}

public class OrderLine
{
    public Product Product { get; init; }
    public long Quantity { get; init; }
}

public class Organization
{
    public string Name { get; init; }
    public List<User> Users { get; init; }
    public List<Order> Orders { get; init; }
}

public class PhysicalProduct
{
    public string Type { get; init; }
    public string Sku { get; init; }
    public decimal Weight { get; init; }
}

public class Product
{
    public ProductvalueBase Value { get; init; }
}

public class User
{
    public string Id { get; init; }
    public string Name { get; init; }
    public string Email { get; init; }
    public UserStatus Status { get; init; }
    public List<Role> Roles { get; init; }
    public Address? Address { get; init; }
}
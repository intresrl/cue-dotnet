using System;
using System.Collections.Generic;

public class Item
{
    public string Id { get; init; }
    public string Name { get; init; }
}

public class Root
{
    public string UserId { get; init; }
    public string OrganizationId { get; init; }
    public string OrganizationName { get; init; }
    public bool IsEnabled { get; init; }
    public List<Item> Roles { get; init; }
}
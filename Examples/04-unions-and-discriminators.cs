using System;
using System.Collections.Generic;
using System.Numerics;

public interface ContactvalueBase
{
    public record AsEmailContact(EmailContact value) : ContactvalueBase;
    public record AsPhoneContact(PhoneContact value) : ContactvalueBase;
    public record Value(ContactvalueBase[] Branches)
    {
        public bool Valid => Branches.Length == 1;
    };
}

public interface DrawingshapeBase
{
    public record AsCircle(Circle value) : DrawingshapeBase;
    public record AsRectangle(Rectangle value) : DrawingshapeBase;
    public record Value(DrawingshapeBase[] Branches)
    {
        public bool Valid => Branches.Length == 1;
    };
}

public interface EventvalueBase
{
    public record AsCreatedEvent(CreatedEvent value) : EventvalueBase;
    public record AsDeletedEvent(DeletedEvent value) : EventvalueBase;
    public record Value(EventvalueBase[] Branches)
    {
        public bool Valid => Branches.Length == 1;
    };
}

public interface PetvalueBase
{
    public record AsCat(Cat value) : PetvalueBase;
    public record AsDog(Dog value) : PetvalueBase;
    public record AsBird(Bird value) : PetvalueBase;
    public record Value(PetvalueBase[] Branches)
    {
        public bool Valid => Branches.Length == 1;
    };
}

public interface StringOrIntegervalueBase
{
    public record AsStringOrIntegervalue(StringOrIntegervalue value) : StringOrIntegervalueBase;
    public record AsStringOrIntegervalue(StringOrIntegervalue value) : StringOrIntegervalueBase;
    public record Value(StringOrIntegervalueBase[] Branches)
    {
        public bool Valid => Branches.Length == 1;
    };
}

public class Bird
{
    public string Type { get; init; }
    public string Name { get; init; }
    public bool CanFly { get; init; }
}

public class Cat
{
    public string Type { get; init; }
    public string Name { get; init; }
    public long Lives { get; init; }
}

public class Circle
{
    public string Kind { get; init; }
    public decimal Radius { get; init; }
}

public class Contact
{
    public ContactvalueBase Value { get; init; }
}

public class CreatedEvent
{
    public string Status { get; init; }
    public string Id { get; init; }
}

public class DeletedEvent
{
    public string Status { get; init; }
    public string Id { get; init; }
}

public class Dog
{
    public string Type { get; init; }
    public string Name { get; init; }
    public string Breed { get; init; }
}

public class Drawing
{
    public string Name { get; init; }
    public DrawingshapeBase Shape { get; init; }
}

public class EmailContact
{
    public string Address { get; init; }
}

public class Event
{
    public EventvalueBase Value { get; init; }
}

public class Pet
{
    public PetvalueBase Value { get; init; }
}

public class PhoneContact
{
    public string Number { get; init; }
}

public class Rectangle
{
    public string Kind { get; init; }
    public decimal Width { get; init; }
    public decimal Height { get; init; }
}

public class StringOrInteger
{
    public StringOrIntegervalueBase Value { get; init; }
}

public class UnionsAndDiscriminatorsExample
{
    public StringOrInteger PrimitiveUnion { get; init; }
    public Contact Contact { get; init; }
    public Pet Pet { get; init; }
    public Event Event { get; init; }
    public Drawing Drawing { get; init; }
    public Zoo Zoo { get; init; }
}

public class Zoo
{
    public List<PetvalueBase> Animals { get; init; }
}
using System;
using System.Collections.Generic;
using System.Numerics;
using ExtendedNumerics;
using System.Text.RegularExpressions;

namespace Examples.unions_and_discriminators
{
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
        public record AsString(string value) : StringOrIntegervalueBase;
        public record AsInt(long value) : StringOrIntegervalueBase;
        public record Value(StringOrIntegervalueBase[] Branches)
        {
            public bool Valid => Branches.Length == 1;
        };
    }

    public class Bird
    {
        public required string Type { get; init; }
        public required string Name { get; init; }
        public required bool CanFly { get; init; }
    }

    public class Cat
    {
        public required string Type { get; init; }
        public required string Name { get; init; }
        public required long Lives { get; init; }
    }

    public class Circle
    {
        public required string Kind { get; init; }
        public required decimal Radius { get; init; }
    }

    public class Contact
    {
        public required ContactvalueBase Value { get; init; }
    }

    public class CreatedEvent
    {
        public required string Status { get; init; }
        public required string Id { get; init; }
    }

    public class DeletedEvent
    {
        public required string Status { get; init; }
        public required string Id { get; init; }
    }

    public class Dog
    {
        public required string Type { get; init; }
        public required string Name { get; init; }
        public string Breed { get; init; }
    }

    public class Drawing
    {
        public required string Name { get; init; }
        public required DrawingshapeBase Shape { get; init; }
    }

    public class EmailContact
    {
        public required string Address { get; init; }
    }

    public class Event
    {
        public required EventvalueBase Value { get; init; }
    }

    public class Pet
    {
        public required PetvalueBase Value { get; init; }
    }

    public class PhoneContact
    {
        public required string Number { get; init; }
    }

    public class Rectangle
    {
        public required string Kind { get; init; }
        public required decimal Width { get; init; }
        public required decimal Height { get; init; }
    }

    public class StringOrInteger
    {
        public required StringOrIntegervalueBase Value { get; init; }
    }

    public class UnionsAndDiscriminatorsExample
    {
        public required StringOrInteger PrimitiveUnion { get; init; }
        public required Contact Contact { get; init; }
        public required Pet Pet { get; init; }
        public required Event Event { get; init; }
        public required Drawing Drawing { get; init; }
        public required Zoo Zoo { get; init; }
    }

    public class Zoo
    {
        public required List<PetvalueBase> Animals { get; init; }
    }
}
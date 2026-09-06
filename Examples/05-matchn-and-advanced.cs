using System;
using System.Collections.Generic;
using System.Numerics;
using ExtendedNumerics;
using System.Text.RegularExpressions;

namespace Examples.matchn_and_advanced
{
    public interface AnnotationValuevalueBase
    {
        public record AsTextValue(TextValue value) : AnnotationValuevalueBase;
        public record AsNumberValue(NumberValue value) : AnnotationValuevalueBase;
        public record AsBooleanValue(BooleanValue value) : AnnotationValuevalueBase;
        public record Value(AnnotationValuevalueBase[] Branches)
        {
            public bool Valid => Branches.Length == 1;
        };
    }

    public interface ContactValuevalueBase
    {
        public record AsEmailValue(EmailValue value) : ContactValuevalueBase;
        public record AsPhoneValue(PhoneValue value) : ContactValuevalueBase;
        public record Value(ContactValuevalueBase[] Branches)
        {
            public bool Valid => Branches.Length == 1;
        };
    }

    public class AnnotationValue
    {
        public required AnnotationValuevalueBase Value { get; init; }
    }

    public class BooleanValue
    {
        public required string Kind { get; init; }
        public required bool Value { get; init; }
    }

    public class CompositeRecord
    {
        public required string Id { get; init; }
        public required AnnotationValue Annotation { get; init; }
        public required List<ContactValue> Contacts { get; init; }
        public string? Description { get; init; }
    }

    public class ContactValue
    {
        public required ContactValuevalueBase Value { get; init; }
    }

    public class EmailValue
    {
        public required string Type { get; init; }
        public required string Address { get; init; }
    }

    public class MatchNAndAdvancedExample
    {
        public required CompositeRecord Record { get; init; }
    }

    public class NumberValue
    {
        public required string Kind { get; init; }
        public required decimal Value { get; init; }
    }

    public class PhoneValue
    {
        public required string Type { get; init; }
        public required string Number { get; init; }
    }

    public class TextValue
    {
        public required string Kind { get; init; }
        public required string Value { get; init; }
    }
}
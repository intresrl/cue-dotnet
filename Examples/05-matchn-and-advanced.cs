using System;
using System.Collections.Generic;

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
    public AnnotationValuevalueBase Value { get; init; }
}

public class BooleanValue
{
    public string Kind { get; init; }
    public bool Value { get; init; }
}

public class CompositeRecord
{
    public string Id { get; init; }
    public AnnotationValue Annotation { get; init; }
    public List<ContactValue> Contacts { get; init; }
    public string? Description { get; init; }
}

public class ContactValue
{
    public ContactValuevalueBase Value { get; init; }
}

public class EmailValue
{
    public string Type { get; init; }
    public string Address { get; init; }
}

public class MatchNAndAdvancedExample
{
    public CompositeRecord Record { get; init; }
}

public class NumberValue
{
    public string Kind { get; init; }
    public decimal Value { get; init; }
}

public class PhoneValue
{
    public string Type { get; init; }
    public string Number { get; init; }
}

public class TextValue
{
    public string Kind { get; init; }
    public string Value { get; init; }
}
using System;
using System.Collections.Generic;
using System.Numerics;
using ExtendedNumerics;
using System.Text.RegularExpressions;

public interface ContactvalueBase
{
    public record AsEmailContact(EmailContact value) : ContactvalueBase;
    public record AsPhoneContact(PhoneContact value) : ContactvalueBase;
    public record Value(ContactvalueBase[] Branches)
    {
        public bool Valid => Branches.Length == 1;
    };
}

public class Contact
{
    public required ContactvalueBase Value { get; init; }
}

public class EmailContact
{
    public required string Address { get; init; }
}

public class PhoneContact
{
    public required string Number { get; init; }
}
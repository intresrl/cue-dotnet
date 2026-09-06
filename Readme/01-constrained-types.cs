using System;
using System.Collections.Generic;
using System.Numerics;
using ExtendedNumerics;
using System.Text.RegularExpressions;

public readonly record struct Age(byte Value)
{
    public static bool IsValid(byte value) => value >= 0 && value <= 150;
}

public readonly record struct CryptographicHash(BigInteger Value)
{
    public static bool IsValid(BigInteger value) => value > 0;
}

public readonly record struct EmailString(string Value)
{
    public static bool IsValid(string value) => Regex.IsMatch(value, "^.+@.+$");
}

public readonly record struct Port(ushort Value)
{
    public static bool IsValid(ushort value) => value >= 1 && value <= 65535;
}

public readonly record struct Precision(BigDecimal Value)
{
    public static bool IsValid(BigDecimal value) => value == BigDecimal.Parse("3.141592653589793238462643383278999999989006698799107083012975956508235442952113852105");
}

public readonly record struct Status(string Value)
{
    public static bool IsValid(string value) => value == "pending" || value == "active" || value == "done";
}

public readonly record struct Timestamp(ulong Value)
{
    public static bool IsValid(ulong value) => value == 1234567890123456789UL;
}
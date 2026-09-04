using System;
using System.Collections.Generic;
using System.Numerics;
using ExtendedNumerics;

public readonly record struct AnyBool(bool Value)
{
    public static bool IsValid(bool value) => true;
}

public readonly record struct AnyString(string Value)
{
    public static bool IsValid(string value) => true;
}

public readonly record struct ByteLike(byte Value)
{
    public static bool IsValid(byte value) => true && value >= 0L && value <= 255L;
}

public readonly record struct ExactBool(bool Value)
{
    public static bool IsValid(bool value) => value == true;
}

public readonly record struct ExactInt(byte Value)
{
    public static bool IsValid(byte value) => value == 42L;
}

public readonly record struct ExactString(string Value)
{
    public static bool IsValid(string value) => value == "active";
}

public readonly record struct Int16Like(short Value)
{
    public static bool IsValid(short value) => true && value >= -32768L && value <= 32767L;
}

public readonly record struct NegativeInt(BigInteger Value)
{
    public static bool IsValid(BigInteger value) => true && value < 0L;
}

public readonly record struct NonNegativeInt(BigInteger Value)
{
    public static bool IsValid(BigInteger value) => true && value >= 0L;
}

public readonly record struct NonPositiveInt(BigInteger Value)
{
    public static bool IsValid(BigInteger value) => true && value <= 0L;
}

public readonly record struct OneOfThree(byte Value)
{
    public static bool IsValid(byte value) => value == 1L || value == 5L || value == 10L;
}

public readonly record struct Port(ushort Value)
{
    public static bool IsValid(ushort value) => true && value >= 1L && value <= 65535L;
}

public readonly record struct PositiveInt(BigInteger Value)
{
    public static bool IsValid(BigInteger value) => true && value > 0L;
}

public readonly record struct SmallInt(byte Value)
{
    public static bool IsValid(byte value) => true && value >= 0L && value <= 100L;
}
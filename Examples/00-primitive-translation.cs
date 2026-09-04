using System.Numerics;

public readonly record struct ExactInt(byte Value)
{
    public static bool IsValid(byte value) =>
        value == 42;
}

public readonly record struct PositiveInt(BigInteger Value)
{
    public static bool IsValid(BigInteger value) =>
        value > 0;
}

public readonly record struct NonNegativeInt(BigInteger Value)
{
    public static bool IsValid(BigInteger value) =>
        value >= 0;
}

public readonly record struct SmallInt(byte Value)
{
    public static bool IsValid(byte value) =>
        value >= 0 &&
        value <= 100;
}

public readonly record struct Percentage(double Value)
{
    public static bool IsValid(double value) =>
        value >= 0 &&
        value <= 100;
}

public readonly record struct Port(ushort Value)
{
    public static bool IsValid(ushort value) =>
        value >= 1 &&
        value <= 65535;
}

public readonly record struct NegativeInt(BigInteger Value)
{
    public static bool IsValid(BigInteger value) =>
        value < 0;
}

public readonly record struct NonPositiveInt(BigInteger Value)
{
    public static bool IsValid(BigInteger value) =>
        value <= 0;
}

public readonly record struct ZeroOrPositiveLarge(BigInteger Value)
{
    public static bool IsValid(BigInteger value) =>
        value == 0 ||
        value >= 1000;
}

public readonly record struct OneOfThree(byte Value)
{
    public static bool IsValid(byte value) =>
        value == 1 ||
        value == 5 ||
        value == 10;
}

public readonly record struct OutsideRange(BigInteger Value)
{
    public static bool IsValid(BigInteger value) =>
        value < 0 ||
        value > 100;
}

public readonly record struct ByteLike(byte Value)
{
    public static bool IsValid(byte value) =>
        value >= 0 &&
        value <= 255;
}

public readonly record struct Int16Like(short Value)
{
    public static bool IsValid(short value) =>
        value >= -32768 &&
        value <= 32767;
}
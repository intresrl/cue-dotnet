using System.Numerics;

namespace Cue.Generator;

public static class BigIntegerExtensions
{
    /// <summary>
    /// BigInteger.Pow variant that supports negative exponents
    /// </summary>
    /// <param name="mantissa">The number to raise to the exponent power.</param>
    /// <param name="exponent">The result of raising value to the exponent power.</param>
    /// <exception cref="InvalidDataException">resulting value is not whole, or exponent is not an int32</exception>
    public static BigInteger Pow(this BigInteger mantissa, long exponent)
    {
        if (mantissa == BigInteger.Zero)
        {
            return BigInteger.Zero;
        }

        switch (exponent)
        {
            case > int.MaxValue or < int.MinValue:
                throw new InvalidDataException("exponent is not an int32");
            case >= 0:
                return BigInteger.Pow(mantissa, (int) exponent);
        }

        var divisor = BigInteger.One << (int)-exponent;
        var (quotient, remainder) = BigInteger.DivRem(mantissa, divisor);
        return remainder == BigInteger.Zero
            ? quotient
            : throw new InvalidDataException("CUE integer has a fractional binary representation.");
    }
}
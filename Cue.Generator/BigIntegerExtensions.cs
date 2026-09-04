using System.Numerics;
using Cuelang.Cue;
using ExtendedNumerics;

namespace Cue.Generator;

public static class CueFloatExtensions
{
    extension(CueFloat value)
    {
        private (BigInteger whole, BigInteger? divisor) Components()
        {
            var (mantissa, twoExponent) = value;
        
            if (mantissa == BigInteger.Zero)
            {
                return (BigInteger.Zero, null);
            }

            return twoExponent switch
            {
                > int.MaxValue or < int.MinValue => throw new InvalidDataException("exponent is not an int32"),
                >= 0 => (mantissa << (int)twoExponent, null),
                < 0 => (mantissa, BigInteger.One << (int)-twoExponent)
            };
        }

        public BigInteger ToBigInteger()
        {
            var (whole, divisor) = value.Components();

            if (divisor == null)
            {
                return whole;
            }
        
            var (quotient, remainder) = BigInteger.DivRem(whole, divisor.Value);
            return remainder == BigInteger.Zero
                ? quotient
                : throw new InvalidDataException("CUE integer has a fractional binary representation.");
        }

        public BigDecimal ToBigDecimal()
        {
            var (whole, divisor) = value.Components();

            return divisor != null
                ? new BigDecimal(whole, 0) / divisor.Value
                : whole;
        }
    }
}
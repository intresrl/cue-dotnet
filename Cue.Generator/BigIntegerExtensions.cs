using System.Numerics;
using Cuelang.Cue;
using ExtendedNumerics;

namespace Cue.Generator;

public static class CueFloatExtensions
{
    extension(CueFloat value)
    {
        private (BigInteger whole, int? inverseTwoExponent) Components()
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
                < 0 => (mantissa, (int)-twoExponent)
            };
        }

        public BigInteger ToBigInteger()
        {
            var (whole, divisor) = value.Components();

            if (divisor == null)
            {
                return whole;
            }
        
            var (quotient, remainder) = BigInteger.DivRem(whole, BigInteger.One << divisor.Value);
            return remainder == BigInteger.Zero
                ? quotient
                : throw new InvalidDataException("CUE integer has a fractional binary representation.");
        }

        public BigDecimal ToBigDecimal()
        {
            var (whole, divisor) = value.Components();

            if (divisor == null)
            {
                return whole;
            }

            var result = new BigDecimal(whole, 0) / (BigInteger.One << divisor.Value);
            
            // consider only number of significant digits in original mantissa
            var wholeBase10Digits = (int) Math.Ceiling(whole.GetBitLength() * Math.Log10(2.0));
            return BigDecimal.Truncate(result, divisor.Value - wholeBase10Digits);
        }
    }
}
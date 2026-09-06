using System;
using System.Collections.Generic;
using System.Numerics;
using ExtendedNumerics;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace Examples.expressions
{
    public sealed class CueList<TConcrete, TAnyIndex>
    {
        public required TConcrete Concrete { get; init; }
        public List<TAnyIndex> AnyIndex { get; init; } = [];
    }

    public readonly struct Slice((long, long) value)
    {
        public (long, long) Value { get; } = value;

        public static implicit operator Slice((long, long) value) => new(value);
    }

    public readonly struct List(CueList<(long, long, long), long> value)
    {
        public CueList<(long, long, long), long> Value { get; } = value;

        public static implicit operator List(CueList<(long, long, long), long> value) => new(value);
    }

    public interface MatchNMessageBase
    {
        public record AsDateTimeMessage(DateTimeMessage value) : MatchNMessageBase;
        public record AsTextMessage(TextMessage value) : MatchNMessageBase;
        public record Value(MatchNMessageBase[] Branches)
        {
            public bool Valid => Branches.Length == 1;
        };
    }

    public interface OrBase
    {
        public record AsInt(long value) : OrBase;
        public record AsString(string value) : OrBase;
        public record Value(OrBase[] Branches)
        {
            public bool Valid => Branches.Length == 1;
        };
    }

    public readonly record struct BoundRegexMatch(string Value)
    {
        public static bool IsValid(string value) => Regex.IsMatch(value, "^x");
    }

    public readonly record struct BoundRegexNotMatch(string Value)
    {
        public static bool IsValid(string value) => !Regex.IsMatch(value, "^x");
    }

    public readonly record struct Default(byte Value)
    {
        public static bool IsValid(byte value) => value == 1 || value == 2;
    }

    public readonly record struct Index(BigInteger Value)
    {
        public static bool IsValid(BigInteger value) => true;
    }

    public readonly record struct Unify(BigInteger Value)
    {
        public static bool IsValid(BigInteger value) => value >= 0;
    }

    public readonly record struct B(bool Value)
    {
        public static bool IsValid(bool value) => true;
    }

    public class DateTimeMessage
    {
        public required string Type { get; init; }
        public required string Format { get; init; }
        public string Timezone { get; init; }
    }

    public class Obj
    {
        public required long A { get; init; }
    }

    public readonly record struct S(string Value)
    {
        public static bool IsValid(string value) => true;
    }

    public class TextMessage
    {
        public required string Type { get; init; }
        public required long MaxLength { get; init; }
        public string Pattern { get; init; }
    }
}
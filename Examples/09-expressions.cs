using System;
using System.Collections.Generic;
using System.Numerics;
using System.Collections.Generic;

public sealed class CueList<TConcrete, TAnyIndex>
{
    public required TConcrete Concrete { get; init; }
    public List<TAnyIndex> AnyIndex { get; init; } = [];
}

public readonly struct Slice((Default, long) value)
{
    public (Default, long) Value { get; } = value;

    public implicit operator Slice((Default, long) value) => new(value);
}

public readonly struct List(CueList<(Default, long, long), long> value)
{
    public CueList<(Default, long, long), long> Value { get; } = value;

    public implicit operator List(CueList<(Default, long, long), long> value) => new(value);
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

public interface MixedTreeBase
{
    public record AsUnify(Unify value) : MixedTreeBase;
    public record AsS(S value) : MixedTreeBase;
    public record Value(MixedTreeBase[] Branches)
    {
        public bool Valid => Branches.Length == 1;
    };
}

public interface OrBase
{
    public record AsOr(Or value) : OrBase;
    public record AsS(S value) : OrBase;
    public record Value(OrBase[] Branches)
    {
        public bool Valid => Branches.Length == 1;
    };
}

public readonly record struct BoundRegexMatch(string Value)
{
    public static bool IsValid(string value) => value == "^x";
}

public readonly record struct BoundRegexNotMatch(string Value)
{
    public static bool IsValid(string value) => value == "^x";
}

public readonly record struct Default(byte Value)
{
    public static bool IsValid(byte value) => value == 1L || value == 2L;
}

public readonly record struct Unify(BigInteger Value)
{
    public static bool IsValid(BigInteger value) => true && value >= 0L;
}

public readonly record struct B(bool Value)
{
    public static bool IsValid(bool value) => true;
}

public class DateTimeMessage
{
    public string Type { get; init; }
    public S Format { get; init; }
    public string Timezone { get; init; }
}

public class Obj
{
    public long A { get; init; }
}

public readonly record struct S(string Value)
{
    public static bool IsValid(string value) => true;
}

public class TextMessage
{
    public string Type { get; init; }
    public Selector MaxLength { get; init; }
    public string Pattern { get; init; }
}
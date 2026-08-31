using System;
using System.Collections.Generic;

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

public class TextMessage
{
    public string Type { get; init; }
    public Selector MaxLength { get; init; }
    public string Pattern { get; init; }
}
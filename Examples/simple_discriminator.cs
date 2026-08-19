using System;
using System.Collections.Generic;

public interface MatchNMessageBase
{
    public record AsMatchNMessage(MatchNMessage value) : MatchNMessageBase;
    public record AsMatchNMessage(MatchNMessage value) : MatchNMessageBase;
    public record Value(MatchNMessageBase[] Branches)
    {
        public bool Valid => Branches.Length == 1;
    };
}

public interface SimpleOrMessageBase
{
    public record AsSimpleOrMessage(SimpleOrMessage value) : SimpleOrMessageBase;
    public record AsSimpleOrMessage(SimpleOrMessage value) : SimpleOrMessageBase;
    public record Value(SimpleOrMessageBase[] Branches)
    {
        public bool Valid => Branches.Length == 1;
    };
}

public class DateTimeMessage
{
    public string Type { get; init; }
    public string Format { get; init; }
    public string Timezone { get; init; }
}

public class Message
{
    public MatchNMessageBase MatchNMessage { get; init; }
    public SimpleOrMessageBase SimpleOrMessage { get; init; }
}

public class TextMessage
{
    public string Type { get; init; }
    public long MaxLength { get; init; }
    public string Pattern { get; init; }
}
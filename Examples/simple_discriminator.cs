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

public interface SimpleOrMessageBase
{
    public record AsDateTimeMessage(DateTimeMessage value) : SimpleOrMessageBase;
    public record AsTextMessage(TextMessage value) : SimpleOrMessageBase;
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
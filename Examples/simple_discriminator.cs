using System;
using System.Collections.Generic;
using System.Numerics;
using ExtendedNumerics;

public interface MessagematchNMessageBase
{
    public record AsDateTimeMessage(DateTimeMessage value) : MessagematchNMessageBase;
    public record AsTextMessage(TextMessage value) : MessagematchNMessageBase;
    public record Value(MessagematchNMessageBase[] Branches)
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
    public MessagematchNMessageBase MatchNMessage { get; init; }
    public MessagematchNMessageBase SimpleOrMessage { get; init; }
}

public class TextMessage
{
    public string Type { get; init; }
    public long MaxLength { get; init; }
    public string Pattern { get; init; }
}
using System.Numerics;

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
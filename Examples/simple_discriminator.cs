using System;
using System.Collections.Generic;

public abstract class MatchNMessageBase
{
}

public abstract class SimpleOrMessageBase
{
}

public class Root
{
    public DateTimeMessage DateTimeMessage { get; init; }
    public TextMessage TextMessage { get; init; }
    public Message Message { get; init; }
}

public class DateTimeMessage : MatchNMessageBase
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

public class MatchNMessage
{
    public string Type { get; init; }
    public string Format { get; init; }
    public string Timezone { get; init; }
}

public class MatchNMessage
{
    public string Type { get; init; }
    public long MaxLength { get; init; }
    public string Pattern { get; init; }
}

public class TextMessage : MatchNMessageBase
{
    public string Type { get; init; }
    public long MaxLength { get; init; }
    public string Pattern { get; init; }
}
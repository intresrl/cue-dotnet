using System;
using System.Collections.Generic;

public abstract class MessageBase
{
}

public class DateTimeMessage : MessageBase
{
    public string Type { get; init; }
    public string Format { get; init; }
}

public class Message
{
    public MessageBase Message { get; init; }
}

public class Root
{
    public DateTimeMessage DateTimeMessage { get; init; }
    public TextMessage TextMessage { get; init; }
    public Message Message { get; init; }
}

public class TextMessage : MessageBase
{
    public string Type { get; init; }
    public long MaxLength { get; init; }
}
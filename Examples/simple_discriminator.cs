using System;
using System.Collections.Generic;
using System.Numerics;
using ExtendedNumerics;
using System.Text.RegularExpressions;

namespace Examples.simple_discriminator
{
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
        public required string Type { get; init; }
        public required string Format { get; init; }
        public string Timezone { get; init; }
    }

    public class Message
    {
        public required MessagematchNMessageBase MatchNMessage { get; init; }
        public required MessagematchNMessageBase SimpleOrMessage { get; init; }
    }

    public class TextMessage
    {
        public required string Type { get; init; }
        public required long MaxLength { get; init; }
        public string Pattern { get; init; }
    }
}
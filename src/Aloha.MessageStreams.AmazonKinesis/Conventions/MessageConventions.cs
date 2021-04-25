using System;
using System.Collections.Generic;
using System.Text;

namespace Aloha.MessageStreams.AmazonKinesis.Conventions
{
    public class MessageConventions : IConventions
    {
        public MessageConventions(Type type, string streamName)
        {
            this.Type = type;
            this.StreamName = streamName;
        }

        public Type Type { get; }

        public string StreamName { get; }
    }
}

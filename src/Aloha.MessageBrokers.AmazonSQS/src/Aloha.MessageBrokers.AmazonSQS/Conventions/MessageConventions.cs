using System;

namespace Aloha.MessageBrokers.AmazonSQS.Conventions
{
    public class MessageConventions : IConventions
    {
        public MessageConventions(Type type, string queue)
        {
            this.Type = type;
            this.Queue = queue;
        }

        public Type Type { get; }

        public string Queue { get; }
    }
}

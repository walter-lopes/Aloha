using System;

namespace Aloha.MessageBrokers.AmazonSQS
{
    public interface IConventions
    {
        Type Type { get; }

        string Queue { get; }
    }
}

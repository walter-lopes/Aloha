using System;

namespace Aloha.MessageBrokers.AmazonSQS
{
    public interface IConventionsProvider
    {
        IConventions Get<T>();
        IConventions Get(Type type);
    }
}

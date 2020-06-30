using System;

namespace Aloha.MessageBrokers.RabbitMQ
{
    public interface IConventionsProvider
    {
        IConventions Get<T>();
        IConventions Get(Type type);
    }
}

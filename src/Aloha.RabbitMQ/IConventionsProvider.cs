using System;

namespace Aloha.RabbitMQ
{
    public interface IConventionsProvider
    {
        IConventions Get<T>();
        IConventions Get(Type type);
    }
}

using System;
using System.Threading.Tasks;

namespace Aloha.RabbitMQ.Messaging
{
    public interface IMessagePublisher
    {
        Task PublishMessageAsync(string correlationContextId, object message, string routingKey);
    }
}

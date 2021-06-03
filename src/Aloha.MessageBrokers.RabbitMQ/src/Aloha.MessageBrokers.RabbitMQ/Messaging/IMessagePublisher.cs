using System;
using System.Threading.Tasks;

namespace Aloha.MessageBrokers.RabbitMQ.Messaging
{
    public interface IMessagePublisher
    {
        Task PublishMessageAsync(string correlationContextId, object message, string routingKey);
    }
}

using System;
using System.Threading.Tasks;

namespace Aloha.RabbitMQ.Messaging
{
    public interface IMessageHandlerCallback
    {
        Task<bool> HandleMessageAsync(string correlationId, string message);
    }
}

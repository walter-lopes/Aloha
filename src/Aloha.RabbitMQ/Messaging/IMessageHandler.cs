namespace Aloha.MessageBrokers.RabbitMQ.Messaging
{
    public interface IMessageHandler
    {
        void Start(IMessageHandlerCallback callback);
        void Stop();
    }
}

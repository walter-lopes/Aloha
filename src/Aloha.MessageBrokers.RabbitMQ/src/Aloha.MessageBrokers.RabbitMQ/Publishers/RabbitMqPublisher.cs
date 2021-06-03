using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aloha.MessageBrokers.RabbitMQ.Publishers
{
    class RabbitMqPublisher : IBusPublisher
    {
        private readonly IRabbitMqClient _client;
        private readonly IConventionsProvider _conventionsProvider;

        public RabbitMqPublisher(IRabbitMqClient client, IConventionsProvider conventionsProvider)
        {
            this._client = client;
            this._conventionsProvider = conventionsProvider;
        }

        public async Task PublishAsync<T>(T message, 
            string messageId = null, string correlationId = null, 
            string spanContext = null, object messageContext = null, 
            IDictionary<string, object> headers = null) where T : class
        {
            await _client.Send(message, _conventionsProvider.Get(message.GetType()), messageId, correlationId, spanContext, messageContext, headers);
        }

        public Task PublishBatchAsync<T>(IEnumerable<T> messages, 
                string messageId = null, string correlationId = null, 
                string spanContext = null, object messageContext = null, 
                IDictionary<string, object> headers = null) where T : class
        {
            throw new System.NotImplementedException();
        }
    }
}

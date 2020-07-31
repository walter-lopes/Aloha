using Aloha.MessageBrokers.AmazonSQS.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aloha.MessageBrokers.AmazonSQS.Publishers
{
    public class AmazonSQSPublisher : IBusPublisher
    {
        private readonly IAmazonSQSClient _amazonSQSClient;

        public AmazonSQSPublisher(IAmazonSQSClient amazonSQSClient)
        {
            _amazonSQSClient = amazonSQSClient;
        }

        public async Task PublishAsync<T>(T message, string messageId = null, string correlationId = null, 
            string spanContext = null, object messageContext = null, 
            IDictionary<string, object> headers = null) where T : class
        {
            var entry = new MessageEntry<T> { Id = Guid.NewGuid().ToString(), Message = message };

            await _amazonSQSClient.EnqueueAsync(entry);
        }

        public async Task PublishBatchAsync<T>(IEnumerable<T> messages, string messageId = null, string correlationId = null,
            string spanContext = null, object messageContext = null, 
            IDictionary<string, object> headers = null) where T : class
        {
            var entries = messages
                            .Select(obj => new MessageEntry<T> { Id = Guid.NewGuid().ToString(), Message = obj });

            await _amazonSQSClient.EnqueueBatchAsync(entries);
        }
    }
}

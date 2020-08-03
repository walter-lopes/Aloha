using Aloha.MessageBrokers.AmazonSQS.Messages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aloha.MessageBrokers.AmazonSQS.Publishers
{
    public class AmazonSQSPublisher : IBusPublisher
    {
        private readonly IAmazonSQSClient _amazonSQSClient;
        private readonly IConventionsProvider _conventions;

        public AmazonSQSPublisher(IAmazonSQSClient amazonSQSClient, IConventionsProvider conventions)
        {
            _amazonSQSClient = amazonSQSClient;
            _conventions = conventions;
        }

        public async Task PublishAsync<T>(T message, string messageId = null, string correlationId = null, 
            string spanContext = null, object messageContext = null, 
            IDictionary<string, object> headers = null) where T : class
        {
            var entry = new MessageEntry<T>(message);

            await _amazonSQSClient.EnqueueAsync(entry, _conventions.Get<T>().Queue);
        }

        public async Task PublishBatchAsync<T>(IEnumerable<T> messages, string messageId = null, string correlationId = null,
            string spanContext = null, object messageContext = null, 
            IDictionary<string, object> headers = null) where T : class
        {
            var entries = messages
                            .Select(obj => new MessageEntry<T>(obj));

            await _amazonSQSClient.EnqueueBatchAsync(entries, _conventions.Get<T>().Queue);
        }
    }
}

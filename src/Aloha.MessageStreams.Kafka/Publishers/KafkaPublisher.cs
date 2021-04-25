using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aloha.MessageStreams.Kafka.Publishers
{
    public class KafkaPublisher : IStreamPublisher
    {
        private readonly IKafkaClient _client;

        public KafkaPublisher(IKafkaClient client)
        {
            _client = client;
        }

        public async Task PublishAsync<T>(T message)
        {
            await _client.Publish();
        }

        public async Task PublishAsync<T>(IEnumerable<T> messages)
        {
            await _client.Publish();
        }
    }
}

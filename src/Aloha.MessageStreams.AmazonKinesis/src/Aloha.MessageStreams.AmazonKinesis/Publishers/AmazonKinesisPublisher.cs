using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aloha.MessageStreams.AmazonKinesis.Publishers
{
    public class AmazonKinesisPublisher : IStreamPublisher
    {
        private readonly IAmazonKinesisClient _amazonKinesisClient;
        private readonly IConventionsProvider _conventionsProvider;

        public AmazonKinesisPublisher(IAmazonKinesisClient amazonKinesisClient, IConventionsProvider conventionsProvider)
        {
            _amazonKinesisClient = amazonKinesisClient;
            _conventionsProvider = conventionsProvider;
        }

        public async Task PublishAsync<T>(T message)
        {
            await _amazonKinesisClient.RecordAsync(message, _conventionsProvider.Get<T>().StreamName);
        }

        public async Task PublishAsync<T>(IEnumerable<T> messages)
        {
            await _amazonKinesisClient.RecordAsync(messages, _conventionsProvider.Get<T>().StreamName);
        }
    }
}

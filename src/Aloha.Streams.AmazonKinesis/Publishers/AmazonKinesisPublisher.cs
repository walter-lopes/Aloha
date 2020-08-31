using System.Threading.Tasks;

namespace Aloha.Streams.AmazonKinesis.Publishers
{
    public class AmazonKinesisPublisher : IStreamPublisher
    {
        private readonly IAmazonKinesisClient _amazonKinesisClient;
        private readonly IConventions _conventions;

        public AmazonKinesisPublisher(IAmazonKinesisClient amazonKinesisClient, IConventions conventions)
        {
            _amazonKinesisClient = amazonKinesisClient;
            _conventions = conventions;
        }

        public async Task PublishAsync<T>(T message)
        {
            await _amazonKinesisClient.RecordAsync(message, _conventions.StreamName);
        }
    }
}

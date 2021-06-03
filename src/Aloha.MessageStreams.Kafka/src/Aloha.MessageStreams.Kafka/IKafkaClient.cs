using System.Threading.Tasks;

namespace Aloha.MessageStreams.Kafka
{
    public interface IKafkaClient
    {
        Task Publish();

        Task Consume();
    }
}

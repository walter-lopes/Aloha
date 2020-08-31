using System.Threading.Tasks;

namespace Aloha.Streams
{
    public interface IStreamPublisher
    {
        Task PublishAsync<T>(T message);
    }
}

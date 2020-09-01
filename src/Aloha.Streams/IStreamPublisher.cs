using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aloha.Streams
{
    public interface IStreamPublisher
    {
        Task PublishAsync<T>(T message);

        Task PublishAsync<T>(IEnumerable<T> messages);
    }
}

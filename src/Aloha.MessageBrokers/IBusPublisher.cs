using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aloha.MessageBrokers
{
    public interface IBusPublisher
    {
        Task PublishAsync<T>(T message, string messageId = null, string correlationId = null, string spanContext = null,
            object messageContext = null, IDictionary<string, object> headers = null) where T : class;

        Task PublishBatchAsync<T>(IEnumerable<T> messages, string messageId = null, string correlationId = null, string spanContext = null,
            object messageContext = null, IDictionary<string, object> headers = null) where T : class;
    }
}

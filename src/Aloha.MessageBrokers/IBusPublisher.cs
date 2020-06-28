using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aloha.MessageBrokers
{
    public interface IBusPublisher
    {
        Task PublishAsync<T>(T message, string mesageId = null, string correlationId = null, string spanContext = null,
            object messageContext = null, IDictionary<string, object> headers = null) where T : class;
    }
}

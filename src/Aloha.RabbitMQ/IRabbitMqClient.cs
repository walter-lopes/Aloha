using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aloha.RabbitMQ
{
    public interface IRabbitMqClient
    {
        Task Send(object message, IConventions conventions, string messageId = null, string correlationId = null,
            string spanContext = null, object messageContext = null, IDictionary<string, object> headers = null);
    }
}

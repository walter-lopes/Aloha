using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aloha.MessageBrokers.AmazonSQS.Publishers
{
    public class AmazonSQSPublisher : IBusPublisher
    {
        public Task PublishAsync<T>(T message, string mesageId = null, string correlationId = null, 
            string spanContext = null, object messageContext = null, IDictionary<string, object> headers = null) where T : class
        {
            throw new NotImplementedException();
        }
    }
}

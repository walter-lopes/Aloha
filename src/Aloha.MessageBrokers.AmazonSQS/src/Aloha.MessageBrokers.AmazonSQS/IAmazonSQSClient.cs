using Aloha.MessageBrokers.AmazonSQS.Messages;
using Aloha.Types;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aloha.MessageBrokers.AmazonSQS
{
    public interface IAmazonSQSClient
    {
        Task<bool> EnqueueBatchAsync<T>(IEnumerable<MessageEntry<T>> messages, string queue = "");

        Task<bool> EnqueueAsync<T>(T obj, string queue = "");

        Task<IEnumerable<MessageEntry<T>>> ReceiveMessageAsync<T>(string queue = "", int maxItems = 1, int timeoutSeconds = 0);

        Task<bool> DeleteMessagesAsync<T>(IEnumerable<MessageEntry<T>> messages, string queueUrl) where T : IIdentifiable<string>;
    }
}

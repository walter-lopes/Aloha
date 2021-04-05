using System;

namespace Aloha.MessageBrokers.AmazonSQS.Messages
{
    public class MessageEntry
    {
        public string Id { get; set; }

        public string UniqueKey { get; set; }

        public string ReceiptHandle { get; set; }
    }

    public class MessageEntry<T>
        : MessageEntry
    {
        public MessageEntry(T message)
        {
            this.Message = message;
            this.Id = Guid.NewGuid().ToString();
        }

        public MessageEntry(string id, T message, string receiptHandle)
        {
            this.Id = id;
            this.Message = message;
            this.ReceiptHandle = receiptHandle;
        }

        public T Message { get; set; }
    }
}

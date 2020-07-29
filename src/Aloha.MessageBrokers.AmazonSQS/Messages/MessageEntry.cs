namespace Aloha.MessageBrokers.AmazonSQS.Messages
{
    public class MessageEntry
    {
        public string Id { get; set; }

        public string ReceiptHandle { get; set; }
    }

    public class MessageEntry<T>
        : MessageEntry
    {
        public T Message { get; set; }
    }
}

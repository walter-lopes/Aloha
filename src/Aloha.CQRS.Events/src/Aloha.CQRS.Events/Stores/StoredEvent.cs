using System;

namespace Aloha.CQRS.Events.Stores
{
    public class StoredEvent
    {
        public StoredEvent(Guid messageId, IEvent payload, DateTime when, Guid who)
        {
            Id = Guid.NewGuid();
            MessageId = messageId;
            Payload = payload;
            When = when;
            Who = who;
        }
        
        
        public StoredEvent(IEvent payload)
        {
            Id = Guid.NewGuid();
            MessageId = payload.MessageId;
            Payload = payload;
            MessageType = payload.GetType().Name;
            When = payload.When;
            Who = payload.Who;
        }

        public Guid Id { get; private set; }

        public Guid MessageId { get; private set; }

        public string MessageType { get;  private set; }

        public IEvent Payload { get; private set; }

        public DateTime When { get; private set; }

        public Guid Who { get; private set; }
    }
}
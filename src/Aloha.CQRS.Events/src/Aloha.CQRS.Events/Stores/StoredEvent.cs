using System;

namespace Aloha.CQRS.Events.Stores
{
    public class StoredEvent
    {
        public StoredEvent(IEvent payload)
        {
            Id = Guid.NewGuid();
            Payload = payload;
            MessageType = payload.GetType().Name;
        }
        
        public StoredEvent(ITenantEvent payload)
        {
            Id = Guid.NewGuid();
            Payload = payload;
            MessageType = payload.GetType().Name;
            TenantId = payload.TenantId;
        }

        public Guid Id { get; private set; }

        public string MessageType { get;  private set; }

        public IEvent Payload { get; private set; }

        public Guid TenantId { get; set; }
    }
}
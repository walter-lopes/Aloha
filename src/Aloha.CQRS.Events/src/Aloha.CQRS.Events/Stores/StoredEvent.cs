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
            SetTenantId(payload);
        }
        
        private void SetTenantId(IEvent payload)
        {
            if (payload is ITenantEvent tenantPayload)
            {
                TenantId = tenantPayload.TenantId;
            }
        }

        public Guid Id { get; private set; }

        public string MessageType { get;  private set; }

        public IEvent Payload { get; private set; }

        public Guid TenantId { get; set; }
    }
}
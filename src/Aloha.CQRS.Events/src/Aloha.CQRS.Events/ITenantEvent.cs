using System;

namespace Aloha.CQRS.Events
{
    public interface ITenantEvent : IEvent
    {
        public Guid TenantId { get; set; }
    }
}
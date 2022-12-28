using Aloha.CQRS.Events;
using System;

namespace Aloha.Services.Customers.Application.Events
{
    public class CustomerCreatedEvent : ITenantEvent
    {
        public CustomerCreatedEvent(Guid customerId)
        {
            CustomerId = customerId;
            TenantId = Guid.NewGuid();
        }

        public Guid CustomerId { get; set; }
        public Guid Who { get; set; }
        public Guid TenantId { get; set; }
    }
}

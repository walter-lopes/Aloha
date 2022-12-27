using Aloha.CQRS.Events;
using System;

namespace Aloha.Services.Customers.Application.Events
{
    public class CustomerCreatedEvent : IEvent
    {
        public CustomerCreatedEvent(Guid customerId)
        {
            CustomerId = customerId;
        }

        public Guid CustomerId { get; set; }
        public Guid Who { get; set; }
        public Guid MessageId { get; set; }
        public string MessageType { get; set; }
    }
}

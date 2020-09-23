using Aloha.CQRS.Events;
using System;

namespace Aloha.Services.Customers.Events
{
    public class CustomerCreated : IEvent
    {
        public Guid CustomerId { get; set; }

        public Guid MessageId { get; set; }
    }
}

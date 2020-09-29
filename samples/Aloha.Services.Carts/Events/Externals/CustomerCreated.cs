using Aloha.CQRS.Events;
using System;

namespace Aloha.Services.Carts.Events.Externals
{
    public class CustomerCreated : IEvent
    {
        public Guid CustomerId { get; set; }
        public Guid MessageId { get ; set ; }
    }
}

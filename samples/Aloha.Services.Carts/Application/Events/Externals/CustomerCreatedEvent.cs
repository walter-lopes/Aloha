using Aloha.CQRS.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aloha.Services.Carts.Application.Events.Externals
{
    public class CustomerCreatedEvent : IEvent
    {
        public Guid CustomerId { get; set; }

        public Guid MessageId { get; set; }
    }
}

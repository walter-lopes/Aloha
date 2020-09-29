using Aloha.CQRS.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aloha.Services.Carts.Application.Events.Externals
{
    public class CustomerCreated : IEvent
    {
        public Guid MessageId { get ; set ; }
    }
}

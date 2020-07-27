
using Aloha.CQRS.Events;
using Aloha.CQRS.Notifications;
using Aloha.Notifications;
using System;
using System.Threading.Tasks;

namespace Aloha.Services.Carts.Events.External.Handlers
{
    public class CustomerEventHandler : IEventHandler<CustomerCreatedEvent>
    {
      
        public CustomerEventHandler()
        {
           
        }
        public async Task HandleAsync(CustomerCreatedEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}

using Aloha.CQRS.Events;
using System;
using System.Threading.Tasks;

namespace Aloha.Services.Carts.Events.External.Handlers
{
    public class CustomerEventHandler : IEventHandler<CustomerCreatedEvent>
    {
        public async Task HandleAsync(CustomerCreatedEvent @event)
        {
            Console.WriteLine($"Received event: {@event.CustomerId}");
        }
    }
}

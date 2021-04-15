using Aloha.CQRS.Events;
using Aloha.Persistence.MongoDB;
using Aloha.Services.Carts.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aloha.Services.Carts.Application.Events.Externals.Handlers
{
    public class CustomerCreatedEventHandler : IEventHandler<CustomerCreatedEvent>
    {
        private readonly IMongoRepository<Cart, Guid> _mongoRepository;

        public CustomerCreatedEventHandler(IMongoRepository<Cart, Guid> mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }
        public async Task HandleAsync(CustomerCreatedEvent @event)
        {
            var cart = new Cart(@event.CustomerId);

            await _mongoRepository.AddAsync(cart);
        }
    }
}

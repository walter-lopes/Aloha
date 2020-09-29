using Aloha.CQRS.Events;
using Aloha.Persistence.MongoDB;
using Aloha.Services.Carts.Domain;
using System;
using System.Threading.Tasks;

namespace Aloha.Services.Carts.Events.Externals.Handlers
{
    public class CustomerCreatedHandler : IEventHandler<CustomerCreated>
    {
        private readonly IMongoRepository<Cart, Guid> _repository;

        public CustomerCreatedHandler(IMongoRepository<Cart, Guid> repository)
        {
            _repository = repository;
        }

        public async Task HandleAsync(CustomerCreated @event)
        {
            var cart = new Cart(@event.CustomerId);

            await _repository.AddAsync(cart);
        }
    }
}

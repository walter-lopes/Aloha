using Aloha.CQRS.Events;
using Aloha.Services.Carts.Application.Events.Externals;
using Aloha.Services.Carts.Core.Entities;
using System;
using System.Threading.Tasks;

namespace Aloha.Services.Carts.Events.Externals.Handlers
{
    public class CustomerCreatedHandler : IEventHandler<CustomerCreated>
    {
      //  private readonly IMongoRepository<Cart, Guid> _repository;

        public CustomerCreatedHandler()
        {
           
        }

        public async Task HandleAsync(CustomerCreated @event)
        {
            var cart = new Cart(@event.CustomerId);

           // await _repository.AddAsync(cart);
        }
    }
}

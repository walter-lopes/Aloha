using Aloha.CQRS.Commands;
using Aloha.CQRS.Events;
using Aloha.Services.Customers.Core.Entities;
using Aloha.Services.Customers.Events;
using System;
using System.Threading.Tasks;

namespace Aloha.Services.Customers.Commands.Handlers
{
    public class CreateCustomerHandler : ICommandHandler<CreateCustomer>
    {
        private readonly IEventDispatcher _dispatcher;
       // private readonly IMongoRepository<Customer, Guid> _customerRepository;

        public CreateCustomerHandler(IEventDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
           
        }

        public async Task HandleAsync(CreateCustomer command)
        {
            var customer = new Customer(command.Email);

           // await _customerRepository.AddAsync(customer);

            var @event = new CustomerCreated(customer.Id);

            await _dispatcher.PublishAsync(@event);
        }
    }
}

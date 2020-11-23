using Aloha.CQRS.Commands;
using Aloha.CQRS.Events;
using Aloha.CQRS.Notifications;
using Aloha.Notifications;
using Aloha.Persistence.MongoDB;
using Aloha.Services.Customers.Core.Entities;
using Aloha.Services.Customers.Events;
using System;
using System.Threading.Tasks;

namespace Aloha.Services.Customers.Commands.Handlers
{
    public class CreateCustomerHandler : ICommandHandler<CreateCustomer>
    {
        private readonly IEventDispatcher _dispatcher;
        private readonly IMongoRepository<Customer, Guid> _customerRepository;

        public CreateCustomerHandler(IEventDispatcher dispatcher, IMongoRepository<Customer, Guid> customerRepository)
        {
            _dispatcher = dispatcher;
            _customerRepository = customerRepository;
        }

        public async Task HandleAsync(CreateCustomer command)
        {
            var customer = new Customer(command.Email);

            await _customerRepository.AddAsync(customer);

            var @event = new CustomerCreated(customer.Id);

            await _dispatcher.PublishAsync(@event);
        }
    }
}

using Aloha.CQRS.Commands;
using Aloha.CQRS.Events;
using Aloha.MessageBrokers;
using Aloha.Persistence.MongoDB;
using Aloha.Services.Customers.Domain;
using Aloha.Services.Customers.Events;
using System;
using System.Threading.Tasks;

namespace Aloha.Services.Customers.Commands.Handlers
{
    public class CustomerCommandHandler : ICommandHandler<CreateCustomerCommand>, 
        ICommandHandler<UpdateCustomerCommand>
    {
        private readonly IEventDispatcher _dispatcher;
        private readonly IMongoRepository<Customer, Guid> _repository;

        public CustomerCommandHandler(IEventDispatcher dispatcher, IMongoRepository<Customer, Guid> repository)
        {
            _repository = repository;
            _dispatcher = dispatcher;
        }

        public async Task HandleAsync(CreateCustomerCommand command)
        {
            var customer = new Customer(command.Email);

            Console.WriteLine($"Customer created: {customer.Id}");

            await _repository.AddAsync(customer);

            var @event = new CustomerCreated() { CustomerId = customer.Id };

            await _dispatcher.PublishAsync(@event);
        }

        public Task HandleAsync(UpdateCustomerCommand command)
        {
            Console.WriteLine($"Updating customer: {command.Email}");

            return Task.CompletedTask;
        }
    }
}

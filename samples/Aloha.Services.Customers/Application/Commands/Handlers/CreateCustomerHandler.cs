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

        public CreateCustomerHandler(IEventDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public async Task HandleAsync(CreateCustomer command)
        {
            var customer = new Customer(command.Email);

            Console.WriteLine($"Customer created: {customer.Id}");

            var @event = new CustomerCreated(customer.Id);

            await _dispatcher.PublishAsync(@event);
        }
    }
}

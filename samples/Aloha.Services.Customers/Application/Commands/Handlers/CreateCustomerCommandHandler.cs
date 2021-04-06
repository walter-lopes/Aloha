using Aloha.CQRS.Commands;
using Aloha.CQRS.Events;
using Aloha.Services.Customers.Core.Entities;
using Aloha.Services.Customers.Events;
using System.Threading.Tasks;

namespace Aloha.Services.Customers.Commands.Handlers
{
    public class CreateCustomerCommandHandler : ICommandHandler<CreateCustomerCommand>
    {
        private readonly IEventDispatcher _eventDispatcher;

        public CreateCustomerCommandHandler(IEventDispatcher eventDispatcher)
        {
            _eventDispatcher = eventDispatcher;         
        }

        public async Task HandleAsync(CreateCustomerCommand command)
        {
            var customer = new Customer(command.Email);

            var @event = new CustomerCreated(customer.Id);

            await _eventDispatcher.PublishAsync(@event);
        }
    }
}

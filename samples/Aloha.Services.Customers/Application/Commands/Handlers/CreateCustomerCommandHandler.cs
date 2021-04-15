using Aloha.CQRS.Commands;
using Aloha.CQRS.Events;
using Aloha.Persistence.MongoDB;
using Aloha.Services.Customers.Application.Events;
using Aloha.Services.Customers.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aloha.Services.Customers.Application.Commands.Handlers
{
    public class CreateCustomerCommandHandler : ICommandHandler<CreateCustomerCommand>
    {
        private readonly IMongoRepository<Customer, Guid> _mongoRepository;
        private readonly IEventDispatcher _eventDispatcher;

        public CreateCustomerCommandHandler(IMongoRepository<Customer, Guid> mongoRepository,
            IEventDispatcher eventDispatcher)
        {
            _mongoRepository = mongoRepository;
            _eventDispatcher = eventDispatcher;
        }

        public async Task HandleAsync(CreateCustomerCommand command)
        {
            var customer = new Customer(command.Name);

            await _mongoRepository.AddAsync(customer);

            var @event = new CustomerCreatedEvent(customer.Id);

            await _eventDispatcher.PublishAsync(@event);
        }
    }
}

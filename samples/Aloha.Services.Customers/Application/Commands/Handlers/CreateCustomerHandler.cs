using Aloha.CQRS.Commands;
using Aloha.CQRS.Events;
using Aloha.CQRS.Notifications;
using Aloha.Notifications;
using Aloha.Services.Customers.Core.Entities;
using Aloha.Services.Customers.Events;
using System;
using System.Threading.Tasks;

namespace Aloha.Services.Customers.Commands.Handlers
{
    public class CreateCustomerHandler : ICommandHandler<CreateCustomer>
    {
        private readonly IEventDispatcher _dispatcher;
        private readonly INotificationDispatcher _notification;

        public CreateCustomerHandler(IEventDispatcher dispatcher, INotificationDispatcher notification)
        {
            _dispatcher = dispatcher;
            _notification = notification;
        }

        public async Task HandleAsync(CreateCustomer command)
        {
            if (!command.IsValid())
            {
                await _notification.PublishAsync(new DomainNotification("ble", "ble"));
            }

            var customer = new Customer(command.Email);

            Console.WriteLine($"Customer created: {customer.Id}");

            var @event = new CustomerCreated(customer.Id);

            await _dispatcher.PublishAsync(@event);
        }
    }
}

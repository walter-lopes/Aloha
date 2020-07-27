using Aloha.CQRS.Commands;
using Aloha.CQRS.Notifications;
using Aloha.Notifications;
using System;
using System.Threading.Tasks;

namespace Aloha.Services.Carts.Commands.Handlers
{
    public class CartCommandHandler : ICommandHandler<AddCartCommand>
    {
        private readonly INotificationDispatcher _notificationDispatcher;

        public CartCommandHandler(INotificationDispatcher notificationDispatcher)
        {
            _notificationDispatcher = notificationDispatcher;
        }

        public async Task HandleAsync(AddCartCommand command)
        {
            await _notificationDispatcher.PublishAsync(new DomainNotification("wrong", "error"));
           
        }
    }
}

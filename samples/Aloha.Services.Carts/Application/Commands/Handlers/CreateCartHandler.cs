using Aloha.CQRS.Commands;
using Aloha.CQRS.Notifications;
using Aloha.Notifications;
using System;
using System.Threading.Tasks;

namespace Aloha.Services.Carts.Application.Commands.Handlers
{
    public class CreateCartHandler : ICommandHandler<CreateCart>
    {
        private readonly INotificationDispatcher _notificationDispatcher;

        public CreateCartHandler(INotificationDispatcher notificationDispatcher)
        {
            _notificationDispatcher = notificationDispatcher;
        }

        public async Task HandleAsync(CreateCart command)
        {
            await _notificationDispatcher.PublishAsync(new DomainNotification("wrong", "error"));
           
        }
    }
}

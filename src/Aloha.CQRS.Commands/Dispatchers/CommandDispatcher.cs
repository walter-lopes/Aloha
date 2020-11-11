using Aloha.CQRS.Notifications;
using Aloha.Notifications;
using DryIoc;
using System.Threading.Tasks;

namespace Aloha.CQRS.Commands.Dispatchers
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IContainer _serviceFactory;
        private readonly INotificationDispatcher _notification;

        public CommandDispatcher(IContainer serviceFactory, INotificationDispatcher notification)
        {
            _serviceFactory = serviceFactory;
            _notification = notification;
        }

        public async Task SendAsync<T>(T command) where T : class, ICommand
        {
            var handler = _serviceFactory.Resolve<ICommandHandler<T>>();
            
            if (!command.IsValid())
            {
                await _notification.PublishAsync(new DomainNotification("bla", "bla"));
            }

            await handler.HandleAsync(command);
        }
    }
}

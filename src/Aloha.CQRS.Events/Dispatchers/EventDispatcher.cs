using System.Threading.Tasks;
using DryIoc;
using Microsoft.Extensions.DependencyInjection;

namespace Aloha.CQRS.Events.Dispatchers
{
    internal sealed class EventDispatcher : IEventDispatcher
    {
        private readonly IContainer _container;

        public EventDispatcher(IContainer container)
        {
            _container = container;
        }

        public async Task PublishAsync<T>(T @event) where T : class, IEvent
        {
            using var scope = _container.CreateScope();
            var handlers = scope.ServiceProvider.GetServices<IEventHandler<T>>();
            foreach (var handler in handlers)
            {
                await handler.HandleAsync(@event);
            }
        }
    }
}
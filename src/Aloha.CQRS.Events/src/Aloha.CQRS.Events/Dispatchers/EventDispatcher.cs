using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Aloha.CQRS.Events.Dispatchers
{
    internal sealed class EventDispatcher : IEventDispatcher
    {
        private readonly IServiceProvider _service;

        public EventDispatcher(IServiceProvider service)
        {
            _service = service;
        }

        public async Task PublishAsync<T>(T @event) where T : class, IEvent
        {
            var handlers = _service.GetServices<IEventHandler<T>>();

            foreach (var handler in handlers)
            {
                await handler.HandleAsync(@event);
            }
        }
    }
}
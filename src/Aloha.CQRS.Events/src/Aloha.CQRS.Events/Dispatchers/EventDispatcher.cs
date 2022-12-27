using System;
using System.Threading.Tasks;
using Aloha.CQRS.Events.Stores;
using Microsoft.Extensions.DependencyInjection;

namespace Aloha.CQRS.Events.Dispatchers
{
    internal sealed class EventDispatcher : IEventDispatcher
    {
        private readonly IServiceProvider _service;
        private readonly EventOptions _options;
        private readonly IEventStoreRepository _eventStoreRepository;

        public EventDispatcher(IServiceProvider service, EventOptions options)
        {
            _service = service;
            _options = options;
            if (_options.EventSourcingEnabled)
            {
                _eventStoreRepository = service.GetRequiredService<IEventStoreRepository>();
            }
        }

        public async Task PublishAsync<T>(T @event) where T : class, IEvent
        {
            if (_options.EventSourcingEnabled)
            {
                var storedEvent = new StoredEvent(@event);
                await _eventStoreRepository.Insert(storedEvent);
            }
            
            var handlers = _service.GetServices<IEventHandler<T>>();

            foreach (var handler in handlers)
            {
                await handler.HandleAsync(@event);
            }
        }
    }
}
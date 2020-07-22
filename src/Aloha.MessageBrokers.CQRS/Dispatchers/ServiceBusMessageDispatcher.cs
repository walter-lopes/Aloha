using Aloha.CQRS.Commands;
using Aloha.CQRS.Events;
using System.Threading.Tasks;

namespace Aloha.MessageBrokers.CQRS.Dispatchers
{
    public class ServiceBusMessageDispatcher : ICommandDispatcher, IEventDispatcher
    {
        private readonly IBusPublisher _busPublisher;
        private readonly ICorrelationContextAccessor _accessor;

        public ServiceBusMessageDispatcher(IBusPublisher busPublisher, ICorrelationContextAccessor accessor)
        {
            _busPublisher = busPublisher;
            _accessor = accessor;
        }

        public Task PublishAsync<T>(T @event) where T : class, IEvent 
            => _busPublisher.PublishAsync(@event, _accessor.CorrelationContext);

        public Task SendAsync<T>(T command) where T : class, ICommand
            => _busPublisher.SendAsync(command, _accessor.CorrelationContext);
    }
}

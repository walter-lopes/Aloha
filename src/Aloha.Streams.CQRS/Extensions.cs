using Aloha.CQRS.Events;
using System.Threading.Tasks;

namespace Aloha.Streams.CQRS
{
    public static class Extensions
    {
        public static Task PublishAsync<TEvent>(this IStreamPublisher streamPublisher, TEvent @event, object messageContext)
            where TEvent : class, IEvent
            => streamPublisher.PublishAsync(@event, messageContext: messageContext);
    }
}

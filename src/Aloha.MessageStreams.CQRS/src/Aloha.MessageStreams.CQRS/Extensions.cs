using Aloha.CQRS.Commands;
using Aloha.CQRS.Events;
using Aloha.MessageBrokers.CQRS.Dispatchers;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aloha.MessageStreams.CQRS
{
    public static class Extensions
    {
        public static Task PublishAsync<TEvent>(this IStreamPublisher streamPublisher, TEvent @event, object messageContext)
            where TEvent : class, IEvent
            => streamPublisher.PublishAsync(@event, messageContext: messageContext);

        public static Task PublishAsync<TEvent>(this IStreamPublisher streamPublisher, IEnumerable<TEvent> events, object messageContext)
            where TEvent : class, IEvent
            => streamPublisher.PublishAsync(events, messageContext: messageContext);

        public static IAlohaBuilder AddServiceBusCommandDispatcher(this IAlohaBuilder builder)
        {
            builder.Services.AddScoped<ICommandDispatcher, ServiceBusMessageDispatcher>();
            return builder;
        }

        public static IAlohaBuilder AddServiceBusEventDispatcher(this IAlohaBuilder builder)
        {
            builder.Services.AddScoped<IEventDispatcher, ServiceBusMessageDispatcher>();
            return builder;
        }
    }
}


using Aloha.CQRS.Commands;
using Aloha.CQRS.Events;
using Aloha.MessageBrokers.CQRS.Dispatchers;
using Aloha.Types;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Aloha.MessageBrokers.CQRS
{
    public static class Extensions
    {
        public static Task SendAsync<TCommand>(this IBusPublisher busPublisher, TCommand command, object messageContext)
            where TCommand : class, ICommand
            => busPublisher.PublishAsync(command, messageContext: messageContext);

        public static Task PublishAsync<TEvent>(this IBusPublisher busPublisher, TEvent @event, object messageContext)
            where TEvent : class, IEvent
            => busPublisher.PublishAsync(@event, messageContext: messageContext);

        public static IBusSubscriber SubscribeCommand<T>(this IBusSubscriber busSubscriber) where T : class, ICommand
            => (IBusSubscriber)busSubscriber.Subscribe<T>(async (serviceProvider, command, _) => 
            {
                using var scope = serviceProvider.CreateScope();
                await scope.ServiceProvider.GetRequiredService<ICommandHandler<T>>().HandleAsync(command);
            });

        public static Task<IBusSubscriber> SubscribeEvent<T>(this IBusSubscriber busSubscriber) where T : class, IEvent
            => busSubscriber.Subscribe<T>(async (serviceProvider, @event, _) =>
            {
                using var scope = serviceProvider.CreateScope();
                await scope.ServiceProvider.GetRequiredService<IEventHandler<T>>().HandleAsync(@event);
            });

        public static Task<IBusConsumer> ConsumeCommand<T>(this IBusConsumer busConsumer) where T : class, ICommand, IIdentifiable<string>
            => busConsumer.Consume<T>(async (serviceProvider, commands) =>
           {
               using var scope = serviceProvider.CreateScope();
               return await scope.ServiceProvider.GetRequiredService<ICommandBatchHandler<T>>().HandleAsync(commands);
           });


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

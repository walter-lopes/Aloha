using Aloha;
using System;
using Microsoft.Extensions.DependencyInjection;
using Aloha.CQRS.Events.Dispatchers;

namespace Aloha.CQRS.Events
{
    public static class Extensions
    {
        public static IAlohaBuilder AddEventHandlers(this IAlohaBuilder builder)
        {
            builder.Services.Scan(s =>
                s.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
                    .AddClasses(c => c.AssignableTo(typeof(IEventHandler<>)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime());

            return builder;
        }

        public static IAlohaBuilder AddInMemoryEventDispatcher(this IAlohaBuilder builder)
        {
            builder.Services.AddSingleton<IEventDispatcher, EventDispatcher>();
            return builder;
        }
    }
}
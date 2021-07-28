using System;
using Aloha.CQRS.Events.Dispatchers;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

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
                     .WithScopedLifetime());

            return builder;
        }

        public static IAlohaBuilder AddInMemoryEventDispatcher(this IAlohaBuilder builder)
        {
            builder.Services.AddScoped<IEventDispatcher, EventDispatcher>();
            return builder;
        }
    }
}
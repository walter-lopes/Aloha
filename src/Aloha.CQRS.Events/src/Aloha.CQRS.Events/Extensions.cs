using System;
using Aloha.CQRS.Events.Dispatchers;
using System.Linq;
using Aloha.CQRS.Events.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Aloha.CQRS.Events
{
    public static class Extensions
    {
        private const string SectionName = "events"; 
        
        public static IAlohaBuilder AddEventHandlers(this IAlohaBuilder builder)
        {
            builder.Services.Scan(s =>
                 s.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
                     .AddClasses(c => c.AssignableTo(typeof(IEventHandler<>)))
                     .AsImplementedInterfaces()
                     .WithScopedLifetime());

            return builder;
        }

        public static IAlohaBuilder AddInMemoryEventDispatcher(this IAlohaBuilder builder, string sectionName = SectionName)
        {
            var options = builder.GetOptions<EventOptions>(sectionName);
            builder.Services.AddSingleton(options);

            if (options.EventSourcingEnabled)
            {
                builder.Services.AddScoped<IDbEventsContext>(sp => new MongoContext
                {
                    ConnectionString = options.ConnectionString,
                    DataBase = options.Database
                });
                builder.Services.AddScoped<IEventStoreRepository, EventStoreRepository>();
            }

            builder.Services.AddScoped<IEventDispatcher, EventDispatcher>();
            return builder;
        }
    }
}
using System;
using Aloha.CQRS.Events.Dispatchers;
using System.Linq;
using DryIoc;

namespace Aloha.CQRS.Events
{
    public static class Extensions
    {
        public static IAlohaBuilder AddEventHandlers(this IAlohaBuilder builder)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                                    .SelectMany(x => x.GetTypes())
                                       .Where(t => t.GetInterfaces()
                                           .Any(i => i.IsGenericType && i.GetGenericTypeDefinition().Equals(typeof(IEventHandler<>))))
                                       .Select(x => x.Assembly);


            builder.Container.RegisterMany(assemblies,
                                getServiceTypes: implType => implType.GetImplementedServiceTypes());

            return builder;
        }

        public static IAlohaBuilder AddInMemoryEventDispatcher(this IAlohaBuilder builder)
        {
            builder.Container.Register<IEventDispatcher, EventDispatcher>(Reuse.Scoped);
            return builder;
        }
    }
}
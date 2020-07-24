using Aloha.CQRS.Commands.Dispatchers;
using DryIoc;
using System;
using System.Linq;

namespace Aloha.CQRS.Commands
{
    public static class Extensions
    {
        public static IAlohaBuilder AddCommandHandlers(this IAlohaBuilder builder)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                                    .Where(t => t.GetInterfaces()
                                        .Any(i => i.IsGenericType && i.GetGenericTypeDefinition().Equals(typeof(ICommandHandler<>)))).Select(x => x.Assembly);

            builder.Container.RegisterMany(assemblies, 
                                getServiceTypes: implType => implType.GetImplementedServiceTypes());

            return builder;
        }

        public static IAlohaBuilder AddInMemoryCommandDispatcher(this IAlohaBuilder builder)
        {
            builder.Container.Register<ICommandDispatcher, CommandDispatcher>(Reuse.Scoped);
            return builder;
        }
    }
}

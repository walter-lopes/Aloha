using Aloha.CQRS.Queries.Dispatchers;
using DryIoc;
using System;
using System.Linq;

namespace Aloha.CQRS.Queries
{
    public static class Extensions
    {
        public static IAlohaBuilder AddQueryHandlers(this IAlohaBuilder builder)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                                  .SelectMany(x => x.GetTypes())
                                      .Where(t => t.GetInterfaces()
                                          .Any(i => i.IsGenericType && i.GetGenericTypeDefinition().Equals(typeof(IQueryHandler<,>))))
                                      .Select(x => x.Assembly);


            builder.Container.RegisterMany(assemblies,
                                getServiceTypes: implType => implType.GetImplementedServiceTypes());

            return builder;
        }

        public static IAlohaBuilder AddInMemoryQueryDispatcher(this IAlohaBuilder builder)
        {
            builder.Container.Register<IQueryDispatcher, QueryDispatcher>(Reuse.Scoped);
            return builder;
        }
    }
}

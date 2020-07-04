using Microsoft.Extensions.DependencyInjection;
using System;

namespace Aloha.CQRS.Queries
{
    public static class Extensions
    {
        public static IAlohaBuilder AddQueryHandlers(this IAlohaBuilder builder)
        {
            builder.Services.Scan(s =>
                s.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
                .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
                .AsImplementedInterfaces()
                .WithTransientLifetime());

            return builder;
        }

        public static IAlohaBuilder AddInMemoryQueryDispatcher(this IAlohaBuilder builder)
        {
            builder.Services.AddSingleton<IQueryDispatcher, IQueryDispatcher>();
            return builder;
        }
    }
}

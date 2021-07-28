using Aloha.CQRS.Commands.Dispatchers;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace Aloha.CQRS.Commands
{
    public static class Extensions
    {
        public static IAlohaBuilder AddCommandHandlers(this IAlohaBuilder builder)
        {
            builder.Services.Scan(s =>
                  s.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
                      .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)))
                      .AsImplementedInterfaces()
                      .WithScopedLifetime());

            builder.Services.Scan(s =>
                  s.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
                      .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<,>)))
                      .AsImplementedInterfaces()
                      .WithScopedLifetime());

            return builder;
        }
        public static IAlohaBuilder AddInMemoryCommandDispatcher(this IAlohaBuilder builder)
        {
            builder.Services.AddScoped<ICommandDispatcher, CommandDispatcher>();
            return builder;
        }
    }
}

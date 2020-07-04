using Aloha.CQRS.Commands.Dispatchers;
using Microsoft.Extensions.DependencyInjection;
using System;

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
            .WithTransientLifetime());

            return builder;
        }

        public static IAlohaBuilder AddInMemoryCommandDispatcher(this IAlohaBuilder builder)
        {
            builder.Services.AddSingleton<ICommandDispatcher, CommandDispatcher>();
            return builder;
        }
    }
}

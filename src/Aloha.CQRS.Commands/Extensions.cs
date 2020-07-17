using Aloha.CQRS.Commands.Dispatchers;
using DryIoc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace Aloha.CQRS.Commands
{
    public static class Extensions
    {
        public static IAlohaBuilder AddCommandHandlers(this IAlohaBuilder builder)
        {
            builder.Services.Register(typeof(ICommandHandler<>), Reuse.Transient,
                made: FactoryMethod.ConstructorWithResolvableArgumentsIncludingNonPublic);

            return builder;
        }

        public static IAlohaBuilder AddInMemoryCommandDispatcher(this IAlohaBuilder builder)
        {
            builder.Services.Register<ICommandDispatcher, CommandDispatcher>(Reuse.Singleton);
            return builder;
        }
    }
}

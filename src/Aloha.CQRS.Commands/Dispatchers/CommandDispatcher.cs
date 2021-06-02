using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Aloha.CQRS.Commands.Dispatchers
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceProvider _serviceFactory;

        public CommandDispatcher(IServiceProvider serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        public async Task SendAsync<T>(T command) where T : class, ICommand
        {
            var handler = _serviceFactory.GetRequiredService<ICommandHandler<T>>();

            await handler.HandleAsync(command);
        }
    }
}

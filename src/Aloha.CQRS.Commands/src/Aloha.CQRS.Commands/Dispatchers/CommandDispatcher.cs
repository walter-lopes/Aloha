using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Aloha.CQRS.Commands.Dispatchers
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public CommandDispatcher(IServiceProvider serviceFactory)
        {
            _serviceProvider = serviceFactory;
        }

        public async Task SendAsync<T>(T command) where T : class, ICommand
        {
            var handler = _serviceProvider.GetRequiredService<ICommandHandler<T>>();

            await handler.HandleAsync(command);
        }

        public async Task<TResponse> SendAsync<TResponse>(ICommand<TResponse> command)
        {
            var handlerType = typeof(ICommandHandler<,>).MakeGenericType(command.GetType(), typeof(TResponse));
            dynamic handler = _serviceProvider.GetRequiredService(handlerType);
            return await handler.HandleAsync((dynamic)command);
        }
    }
}

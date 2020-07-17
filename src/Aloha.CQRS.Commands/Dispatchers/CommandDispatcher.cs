using DryIoc;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Aloha.CQRS.Commands.Dispatchers
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IContainer _serviceFactory;

        public CommandDispatcher(IContainer serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        public async Task SendAsync<T>(T command) where T : class, ICommand
        {
            try
            {
                using var scope = _serviceFactory.CreateScope();
                var handler = scope.ServiceProvider.GetRequiredService<ICommandHandler<T>>();
                await handler.HandleAsync(command);
            }
            catch (System.Exception ex)
            {

                throw;
            }
           
        }
    }
}

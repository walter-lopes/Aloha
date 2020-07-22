using DryIoc;
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
            var handler = _serviceFactory.Resolve<ICommandHandler<T>>();
            await handler.HandleAsync(command);
        }
    }
}

using System.Threading.Tasks;

namespace Aloha.CQRS.Commands
{
    public interface ICommandDispatcher
    {
        Task SendAsync<T>(T command) where T : class, ICommand;

        Task<TResponse> SendAsync<TResponse>(ICommand<TResponse> command);
    }
}

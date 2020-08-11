using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aloha.CQRS.Commands
{
    public interface ICommandHandler<in TCommand> where TCommand : class, ICommand
    {
        Task HandleAsync(TCommand command);
    }

    public interface IBatchCommandHandler<in TCommand> where TCommand : class, ICommand
    {
        Task HandleAsync(IEnumerable<TCommand> commands);
    }
}

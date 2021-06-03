using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aloha.CQRS.Commands
{
    public interface ICommandHandler<in TCommand> where TCommand : class, ICommand
    {
        Task HandleAsync(TCommand command);
    }

    public interface ICommandHandler<TCommand, TResult> where TCommand : class, ICommand<TResult>
    {
        Task<TResult> HandleAsync(TCommand command);
    }

    public interface ICommandBatchHandler<TCommand> where TCommand : class, ICommand
    {
        Task<IEnumerable<TCommand>> HandleAsync(IEnumerable<TCommand> commands);
    }
}

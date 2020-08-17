using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aloha.CQRS.Commands
{
    public interface ICommandHandler<in TCommand> where TCommand : class, ICommand
    {
        Task HandleAsync(TCommand command);
    }

    public interface IBatchCommandHandler<TCommand> where TCommand : class, ICommand
    {
        Task HandleAsync(IEnumerable<TCommand> commands, Func<IEnumerable<TCommand>, Task> onError = null);
    }
}

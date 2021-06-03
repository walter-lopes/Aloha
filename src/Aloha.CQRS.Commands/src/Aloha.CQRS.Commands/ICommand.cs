using System.Collections.Generic;

namespace Aloha.CQRS.Commands
{
    public interface ICommand
    {
        bool IsValid();
    }
}

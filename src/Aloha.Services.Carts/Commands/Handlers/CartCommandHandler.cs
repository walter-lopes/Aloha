using Aloha.CQRS.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aloha.Services.Carts.Commands.Handlers
{
    public class CartCommandHandler : ICommandHandler<AddCartCommand>
    {
        public Task HandleAsync(AddCartCommand command)
        {
            throw new NotImplementedException();
        }
    }
}

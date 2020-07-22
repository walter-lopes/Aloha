using Aloha.CQRS.Commands;
using System;
using System.Threading.Tasks;

namespace Aloha.Services.Carts.Commands.Handlers
{
    public class CartCommandHandler : ICommandHandler<AddCartCommand>
    {
        public CartCommandHandler()
        {

        }

        public Task HandleAsync(AddCartCommand command)
        {
            throw new NotImplementedException();
        }
    }
}

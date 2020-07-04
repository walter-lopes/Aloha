
using Aloha.CQRS.Commands;
using System.Threading.Tasks;

namespace Aloha.Services.Products.Commands.Handlers
{
    public class CreateProductHandler : ICommandHandler<CreateProduct>
    {
        public async Task HandleAsync(CreateProduct command)
        {
            System.Console.WriteLine(command);
        }
    }
}

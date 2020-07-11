
using Aloha.CQRS.Commands;
using Aloha.CQRS.Events;
using System;
using System.Threading.Tasks;

namespace Aloha.Services.Products.Commands.Handlers
{
    public class CreateProductHandler : ICommandHandler<CreateProduct>
    {
        private readonly IEventDispatcher _dispatcher;

        public CreateProductHandler(IEventDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public async Task HandleAsync(CreateProduct command)
        {
           await _dispatcher.PublishAsync(new ProductCreated() { Id = Guid.NewGuid(), Name = "Name" });
        }
    }
}

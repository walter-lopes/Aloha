using Aloha.CQRS.Events;
using System;
using System.Threading.Tasks;

namespace Aloha.Services.Products
{
    public class ProductCreated : IEvent
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid MessageId { get; set; }
    }

    public class ProductEventHandler : IEventHandler<ProductCreated>
    {
        public Task HandleAsync(ProductCreated @event)
        {
            var teste = @event;

            return Task.FromResult(teste);
        }
    }
}

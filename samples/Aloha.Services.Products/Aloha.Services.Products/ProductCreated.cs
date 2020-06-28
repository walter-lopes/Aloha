using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aloha.Services.Products
{
    public class ProductCreated
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }

    public class ProductEventHandler
    {
        public Task HandleAsync(ProductCreated @event)
        {
            var teste = @event;

            return Task.FromResult(teste);
        }
    }
}

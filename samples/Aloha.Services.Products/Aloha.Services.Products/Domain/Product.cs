using Aloha.Types;
using System;

namespace Aloha.Services.Products.Domain
{
    public class Product : IIdentifiable<Guid>
    {
        public Product(string name)
        {
            Name = name;
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}

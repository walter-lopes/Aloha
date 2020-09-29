using Aloha.Types;
using System;

namespace Aloha.Services.Carts.Core.Entities
{
    public class Cart : IIdentifiable<Guid>
    {
        public Cart(Guid customerId)
        {
            this.CustomerId = customerId;
        }

        public Guid Id { get; set; }

        public Guid CustomerId { get; set; }
    }
}

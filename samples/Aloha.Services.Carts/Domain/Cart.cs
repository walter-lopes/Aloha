using Aloha.Types;
using System;

namespace Aloha.Services.Carts.Domain
{
    public class Cart : IIdentifiable<Guid>
    {
        public Cart(Guid customerId)
        {
            this.Id = Guid.NewGuid();
            this.CustomerId = customerId;
        }
        public Guid Id { get; set; }

        public Guid CustomerId { get; set; }
    }
}

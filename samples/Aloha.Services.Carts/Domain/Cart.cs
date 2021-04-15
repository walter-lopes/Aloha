using Aloha.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aloha.Services.Carts.Domain
{
    public class Cart : IIdentifiable<Guid>
    {
        public Cart(Guid customerId)
        {
            CustomerId = customerId;
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }

        public Guid CustomerId { get; set; }
    }
}

using Aloha.Types;
using System;

namespace Aloha.Services.Carts.Domain
{
    public class Cart : IIdentifiable<Guid>
    {
        public Guid Id { get; set; }

        public Guid CustomerId { get; set; }
    }
}

using Aloha.Types;
using System;

namespace Aloha.Services.Customers.Domain
{
    public class Customer : IIdentifiable<Guid>
    {
        public Customer(string email)
        {
            this.Id = Guid.NewGuid();
            this.Email = email;
        }

        public string Email { get; set; }

        public Guid Id { get; set; }
    }
}

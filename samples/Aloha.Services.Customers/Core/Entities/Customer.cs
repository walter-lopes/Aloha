using System;

namespace Aloha.Services.Customers.Core.Entities
{
    public class Customer
    {
        public Customer(string email)
        {
            this.Id = Guid.NewGuid();
            this.Email = email;
            this.Active = false;
        }

        public string Email { get; private set; }

        public Guid Id { get; private set; }

        public bool Active { get; private set; }

        public void Activate() => Active = true;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aloha.Services.Customers.Domain
{
    public class Customer
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

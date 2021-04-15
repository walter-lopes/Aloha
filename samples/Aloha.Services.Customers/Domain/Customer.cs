using Aloha.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aloha.Services.Customers.Domain
{
    public class Customer : IIdentifiable<Guid>
    {
        public Customer(string name)
        {
            Name = name;
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}

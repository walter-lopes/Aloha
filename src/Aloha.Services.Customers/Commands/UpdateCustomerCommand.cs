using Aloha.CQRS.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aloha.Services.Customers.Commands
{
    public class UpdateCustomerCommand : ICommand
    {
        public string Email { get; set; }

        public Guid Id { get; set; }

        public bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}

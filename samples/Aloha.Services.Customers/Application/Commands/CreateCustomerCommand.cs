using Aloha.CQRS.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Aloha.Services.Customers.Application.Commands
{
    public class CreateCustomerCommand : ICommand
    {
        public string Name { get; set; }

        public bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}

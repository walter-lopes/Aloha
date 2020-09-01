using Aloha.CQRS.Commands;
using System;

namespace Aloha.Services.Customers.Commands
{
    public class CreateCustomerCommand : ICommand
    {
        public string Email { get; set; }

        public bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}

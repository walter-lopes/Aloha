using Aloha.CQRS.Commands;
using System;

namespace Aloha.Services.Customers.Commands
{
    public record CreateCustomerCommand : ICommand
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}

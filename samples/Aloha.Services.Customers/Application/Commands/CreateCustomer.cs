using Aloha.CQRS.Commands;
using System;

namespace Aloha.Services.Customers.Commands
{
    public class CreateCustomer : ICommand
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}

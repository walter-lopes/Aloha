using Aloha.CQRS.Commands;
using System;

namespace Aloha.Services.Carts.Application.Commands
{
    public class CreateCart : ICommand
    {
        public string Name { get; set; }

        public bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}

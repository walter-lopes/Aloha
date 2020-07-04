using Aloha.CQRS.Commands;
using System;

namespace Aloha.Services.Products.Commands
{
    public class CreateProduct : ICommand
    {
        public string Name { get; set; }

        public bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}

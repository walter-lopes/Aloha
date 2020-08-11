using Aloha.CQRS.Commands;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aloha.Services.Customers.Commands.Handlers
{
    public class CustomerBatchCommandHandler : IBatchCommandHandler<CreateCustomerCommand>
    {
        public Task HandleAsync(IEnumerable<CreateCustomerCommand> commands)
        {
            throw new NotImplementedException();
        }
    }
}

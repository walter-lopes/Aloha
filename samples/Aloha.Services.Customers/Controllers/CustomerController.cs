using System.Threading.Tasks;
using Aloha.CQRS.Commands;
using Aloha.Services.Customers.Application.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Aloha.Services.Customers.Controllers
{
    [Route("customers")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICommandDispatcher _dispatcher;

        public CustomerController(ICommandDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCustomerCommand command)
        {
            await _dispatcher.SendAsync(command);

            return Accepted();
        }
    }
}

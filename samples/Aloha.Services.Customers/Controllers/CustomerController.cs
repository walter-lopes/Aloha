using System.Threading.Tasks;
using Aloha.CQRS.Commands;
using Aloha.Services.Customers.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Aloha.Services.Customers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICommandDispatcher _dispatcher;

        public CustomerController(ICommandDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCustomer command)
        {
            await _dispatcher.SendAsync(command);

            return Accepted();
        }
    }
}

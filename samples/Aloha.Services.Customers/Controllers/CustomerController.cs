using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aloha.CQRS.Commands;
using Aloha.Services.Customers.Commands;
using Microsoft.AspNetCore.Http;
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
        public async Task<IActionResult> Post([FromBody] CreateCustomerCommand command)
        {
            await _dispatcher.SendAsync(command);

            return Accepted();
        }
    }
}

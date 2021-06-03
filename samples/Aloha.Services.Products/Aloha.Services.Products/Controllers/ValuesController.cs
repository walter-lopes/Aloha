using Microsoft.AspNetCore.Mvc;
using Aloha.CQRS.Commands;
using Aloha.Services.Products.Commands;
using System.Threading.Tasks;

namespace Aloha.Services.Products.Controllers
{
    [Route("values")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ICommandDispatcher _dispatcher;

        public ValuesController(ICommandDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateProduct command)
        {
            await _dispatcher.SendAsync(command);

            return Accepted();        
        }
    }
}

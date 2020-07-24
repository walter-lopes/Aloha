using Aloha.CQRS.Commands;
using Aloha.Services.Carts.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Aloha.Services.Carts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly ICommandDispatcher _dispatcher;

        public CartsController(ICommandDispatcher dispatcher)
        {
           _dispatcher = dispatcher;
        }

        [HttpPost]
        public IActionResult Post([FromBody] AddCartCommand command)
        {
            try
            {
                _dispatcher.SendAsync(command);

                return Accepted();
            }
            catch (System.Exception ex)
            {

                return Ok(ex.Message);
            }
            
        }
    }
}

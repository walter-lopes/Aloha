using Microsoft.AspNetCore.Mvc;

namespace Aloha.Services.Carts.Controllers
{
    [Route("carts")]
    [ApiController]
    public class CartsController : ControllerBase
    {
      
        public CartsController()
        {
           
            
        }

        
        [HttpPost]
        public IActionResult Post()
        {
           return Ok("pong");
        }
    }
}

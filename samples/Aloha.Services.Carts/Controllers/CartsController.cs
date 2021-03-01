using Aloha.CQRS.Commands;
using Aloha.CQRS.Notifications;
using Aloha.CQRS.Notifications.Dispatchers;
using Aloha.Services.Carts.Application.Commands;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Aloha.Services.Carts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
      
        public CartsController()
        {
           
            
        }

        
        [HttpGet("ping")]
        public IActionResult Get()
        {
           return Ok("pong");
        }
    }
}

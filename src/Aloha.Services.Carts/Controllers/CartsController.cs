using Aloha.CQRS.Commands;
using Aloha.CQRS.Notifications;
using Aloha.CQRS.Notifications.Dispatchers;
using Aloha.Services.Carts.Commands;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Aloha.Services.Carts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly ICommandDispatcher _dispatcher;
        private readonly NotificationDispatcher _notificationDispatcher;

        public CartsController(ICommandDispatcher dispatcher, INotificationDispatcher notificationDispatcher)
        {
           _dispatcher = dispatcher;
            _notificationDispatcher = (NotificationDispatcher)notificationDispatcher;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddCartCommand command)
        {
            try
            {
                await _dispatcher.SendAsync(command);

                _notificationDispatcher.HasNotifications();

                return Accepted();
            }
            catch (System.Exception ex)
            {

                return Ok(ex.Message);
            }
            
        }
    }
}

using System.Threading.Tasks;
using Aloha.CQRS.Commands;
using Aloha.CQRS.Notifications;
using Aloha.CQRS.Notifications.Dispatchers;
using Aloha.Services.Customers.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Aloha.Services.Customers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICommandDispatcher _dispatcher;
        private readonly NotificationDispatcher _notificationDispatcher;

        public CustomerController(ICommandDispatcher dispatcher, INotificationDispatcher notificationDispatcher)
        {
            _dispatcher = dispatcher;
            _notificationDispatcher = (NotificationDispatcher) notificationDispatcher;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCustomer command)
        {
            await _dispatcher.SendAsync(command);

            return Accepted();
        }
    }
}

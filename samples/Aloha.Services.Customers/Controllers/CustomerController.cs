using System;
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
        private readonly INotificationDispatcher _notificationDispatcher;
     //   private readonly Serilog.ILogger _logger;

        public CustomerController(ICommandDispatcher dispatcher, INotificationDispatcher notificationDispatcher)
        {
            _dispatcher = dispatcher;
            _notificationDispatcher = (NotificationDispatcher) notificationDispatcher;
           // _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCustomerCommand command)
        {
           

            await _dispatcher.SendAsync(command);

            return Accepted();
        }
    }
}

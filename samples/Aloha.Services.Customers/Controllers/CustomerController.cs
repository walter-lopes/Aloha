using System;
using System.Threading.Tasks;
using Aloha.CQRS.Commands;
using Aloha.CQRS.Notifications;
using Aloha.CQRS.Notifications.Dispatchers;
using Aloha.Logging;
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
        private readonly Serilog.ILogger _logger;

        public CustomerController(ICommandDispatcher dispatcher, INotificationDispatcher notificationDispatcher, Serilog.ILogger logger)
        {
            _dispatcher = dispatcher;
            _notificationDispatcher = (NotificationDispatcher) notificationDispatcher;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCustomer command)
        {
            var _logEvent = new Logging.Loggers.LogEvent(new Guid(), new System.Guid());

            _logger.Information("{TraceId:l}, {ParentSpanId:l}, {SpanId:l}, {EventType:l}", _logEvent.TraceId, _logEvent.ParentSpanId, _logEvent.SpanId, $"{System.Reflection.MethodBase.GetCurrentMethod().ReflectedType?.ReflectedType?.FullName} - Job-Start");

            await _dispatcher.SendAsync(command);

            return Accepted();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Aloha.MessageBrokers;
using Aloha.CQRS.Commands;
using Aloha.Services.Products.Commands;

namespace Aloha.Services.Products.Controllers
{
    [Route("values")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IBusPublisher _publisher;
        private readonly IBusSubscriber _subscriber;
        private readonly ICommandDispatcher _dispatcher;

        public ValuesController(IBusPublisher publisher, IBusSubscriber subscriber, ICommandDispatcher dispatcher)
        {
            _publisher = publisher;
            _subscriber = subscriber;
            _dispatcher = dispatcher;
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] CreateProduct command)
        {
            _dispatcher.SendAsync(command);

            return Accepted();        
        }
    }
}

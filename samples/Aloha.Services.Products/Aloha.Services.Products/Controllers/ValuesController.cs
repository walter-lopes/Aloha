using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Aloha.MessageBrokers;

namespace Aloha.Services.Products.Controllers
{
    [Route("values")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IBusPublisher _publisher;
        private readonly IBusSubscriber _subscriber;

        public ValuesController(IBusPublisher publisher, IBusSubscriber subscriber)
        { 
            _publisher = publisher;
            _subscriber = subscriber;
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> Get()
        {
            try
            {
                await _publisher.PublishAsync(new ProductCreated() { Id = Guid.NewGuid(), Name = "XXT" });

                _subscriber.Subscribe<ProductCreated>(async (serviceProvider, @event, _) =>
                {
                    await new ProductEventHandler().HandleAsync(@event);
                });
            }
            catch (Exception ex)
            {

                throw;
            }
         


            return new List<string>();
        }

          
        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

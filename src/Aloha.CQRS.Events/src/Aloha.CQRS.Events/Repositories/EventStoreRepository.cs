using System.Threading.Tasks;
using Aloha.CQRS.Events.Stores;
using MongoDB.Driver;

namespace Aloha.CQRS.Events.Repositories
{
    public class EventStoreRepository : IEventStoreRepository
    {
        private IMongoCollection<StoredEvent> Collection { get; set; }
        

        public EventStoreRepository(IDbEventsContext context)
        {
            Collection = context.Context.GetCollection<StoredEvent>("events");
        }
        
        public async Task Insert(StoredEvent storedEvent)
        {
            await Collection.InsertOneAsync(@storedEvent);
        }
    }
}
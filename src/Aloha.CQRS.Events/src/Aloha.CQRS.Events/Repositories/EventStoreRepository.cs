using System;
using System.Threading.Tasks;
using Aloha.CQRS.Events.Stores;
using MongoDB.Driver;

namespace Aloha.CQRS.Events.Repositories
{
    public class EventStoreRepository : IEventStoreRepository
    {
        private readonly IDbEventsContext _dbEventsContext;
        private readonly EventOptions _eventOptions;

        public EventStoreRepository(IDbEventsContext context, EventOptions eventOptions)
        {
            _dbEventsContext = context;
            _eventOptions = eventOptions;
        }
        
        public async Task Insert(StoredEvent storedEvent)
        {
            var collection = GetCollection(storedEvent.TenantId);
            
            await collection.InsertOneAsync(storedEvent);
        }
        
        private IMongoCollection<StoredEvent> GetCollection(Guid tenantId)
        {
            if (!_eventOptions.IsMultiTenant) return _dbEventsContext.Context.GetCollection<StoredEvent>("events");
            
            var fullCollectionName = $"{tenantId}_events";
            return _dbEventsContext.Context.GetCollection<StoredEvent>(fullCollectionName);
        }
    }
}
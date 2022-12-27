using MongoDB.Driver;

namespace Aloha.CQRS.Events.Repositories
{
    public interface IDbEventsContext
    {
        IMongoDatabase Context { get; }
    }
}
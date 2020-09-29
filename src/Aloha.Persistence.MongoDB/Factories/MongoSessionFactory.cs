using MongoDB.Driver;
using System.Threading.Tasks;

namespace Aloha.Persistence.MongoDB.Factories
{
    public class MongoSessionFactory : IMongoSessionFactory
    {
        private readonly IMongoClient _client;

        public MongoSessionFactory(IMongoClient client)
            => _client = client;

        public Task<IClientSessionHandle> CreateAsync()
            => _client.StartSessionAsync();
    }
}

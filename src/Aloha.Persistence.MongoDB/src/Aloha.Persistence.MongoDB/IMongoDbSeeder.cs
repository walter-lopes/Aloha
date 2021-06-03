using MongoDB.Driver;
using System.Threading.Tasks;

namespace Aloha.Persistence.MongoDB
{
    public interface IMongoDbSeeder
    {
        Task SeedAsync(IMongoDatabase database);
    }
}

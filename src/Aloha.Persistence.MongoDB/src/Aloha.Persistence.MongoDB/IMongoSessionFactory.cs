using MongoDB.Driver;
using System.Threading.Tasks;

namespace Aloha.Persistence.MongoDB
{
    public interface IMongoSessionFactory
    {
        Task<IClientSessionHandle> CreateAsync();
    }
}

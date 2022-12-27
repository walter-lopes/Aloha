using System.Threading.Tasks;
using Aloha.CQRS.Events.Stores;

namespace Aloha.CQRS.Events
{
    public interface IEventStoreRepository
    {
        Task Insert(StoredEvent storedEvent);
    }
}
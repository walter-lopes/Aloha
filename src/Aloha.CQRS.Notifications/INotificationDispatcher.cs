using Aloha.Notifications;
using System.Threading.Tasks;

namespace Aloha.CQRS.Notifications
{
    public interface INotificationDispatcher
    {
        Task PublishAsync(DomainNotification notification);
    }
}

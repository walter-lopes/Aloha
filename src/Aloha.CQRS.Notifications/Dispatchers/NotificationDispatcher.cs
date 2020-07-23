using Aloha.Notifications;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aloha.CQRS.Notifications.Dispatchers
{
    public class NotificationDispatcher : INotificationDispatcher
    {
        private List<DomainNotification> _notifications;

        public NotificationDispatcher()
        {
            _notifications = new List<DomainNotification>();
        }
        public Task PublishAsync(DomainNotification notification)
        {
            _notifications.Add(notification);

            return Task.CompletedTask;
        }

        public virtual List<DomainNotification> GetNotifications()
            => _notifications;

        public virtual bool HasNotifications()
            => GetNotifications().Any();

        public void Dispose()
           => _notifications = new List<DomainNotification>();
    }
}

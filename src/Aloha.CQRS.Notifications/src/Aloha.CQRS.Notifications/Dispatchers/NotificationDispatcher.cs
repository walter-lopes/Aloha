using Aloha.Notifications;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        /// <summary>
        /// Publish a new domain notification on application
        /// </summary>
        /// <param name="notification">Domain notification to inform an error and code, adding in notifications list</param>
        /// <returns></returns>
        public Task PublishAsync(DomainNotification notification)
        {
            _notifications.Add(notification);

            return Task.CompletedTask;
        }

        /// <summary>
        /// Get all notification in list
        /// </summary>
        /// <returns></returns>
        public virtual List<DomainNotification> GetNotifications()
            => _notifications;

        /// <summary>
        /// Check if contains any notification in list
        /// </summary>
        /// <returns></returns>
        public virtual bool HasNotifications()
            => GetNotifications().Any();

        /// <summary>
        /// Get first http status code from notifications
        /// </summary>
        /// <returns></returns>
        public HttpStatusCode? GetHttpStatusCode()
            => _notifications?.FirstOrDefault().HttpStatusCode;


        public void Dispose()
           => _notifications = new List<DomainNotification>();
    }
}

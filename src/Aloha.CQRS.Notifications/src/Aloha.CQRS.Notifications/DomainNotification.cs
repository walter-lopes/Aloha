using System;
using System.Net;

namespace Aloha.Notifications
{
    public class DomainNotification : INotification
    {
        public Guid DomainNotificationId { get; private set; }

        public HttpStatusCode HttpStatusCode { get; private set; }

        public string Value { get; private set; }

        public DateTime Timestamp { get; private set; }

        public DomainNotification(HttpStatusCode httpStatusCode, string value)
        {
            DomainNotificationId = Guid.NewGuid();
            HttpStatusCode = httpStatusCode;
            Value = value;
            Timestamp = DateTime.UtcNow;
        }
    }
}

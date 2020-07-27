using System;

namespace Aloha.Notifications
{
    public interface INotification
    {
        DateTime Timestamp { get; }
    }
}

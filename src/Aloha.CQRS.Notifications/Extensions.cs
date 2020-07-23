using Aloha.CQRS.Notifications.Dispatchers;
using DryIoc;

namespace Aloha.CQRS.Notifications
{
    public static class Extensions
    {
        public static IAlohaBuilder AddInMemoryNotificationDispatcher(this IAlohaBuilder builder)
        {
            builder.Container.Register<INotificationDispatcher, NotificationDispatcher>(Reuse.Scoped);
            return builder;
        }
    }
}

using Aloha.CQRS.Notifications.Dispatchers;
using Microsoft.Extensions.DependencyInjection;

namespace Aloha.CQRS.Notifications
{
    public static class Extensions
    {
        public static IAlohaBuilder AddInMemoryNotificationDispatcher(this IAlohaBuilder builder)
        {
            builder.Services.AddScoped<INotificationDispatcher, NotificationDispatcher>();
            return builder;
        }
    }
}

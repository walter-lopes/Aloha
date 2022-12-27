using System;

namespace Aloha.CQRS.Events
{
    public interface IEvent
    {
        DateTime When => DateTime.UtcNow;

        Guid Who { set; get; }
        
        Guid MessageId { set; get; }
    }
}

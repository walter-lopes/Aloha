using System;

namespace Aloha.CQRS.Events
{
    public interface IEvent
    {
        DateTime DateTime => DateTime.Now;
        Guid MessageId { set; get; }
    }
}

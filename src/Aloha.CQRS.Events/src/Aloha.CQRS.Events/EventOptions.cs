namespace Aloha.CQRS.Events
{
    public class EventOptions
    {
        public bool EventSourcingEnabled { get; set; } = false;

        public string ConnectionString { get; set; }


        public string Database { get; set; }
    }
}
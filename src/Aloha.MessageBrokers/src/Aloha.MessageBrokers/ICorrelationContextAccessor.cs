namespace Aloha.MessageBrokers
{
    public interface ICorrelationContextAccessor
    {
        object CorrelationContext { get; set; }
    }
}

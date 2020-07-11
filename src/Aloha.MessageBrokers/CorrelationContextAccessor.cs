using System.Threading;

namespace Aloha.MessageBrokers
{
    public class CorrelationContextAccessor : ICorrelationContextAccessor
    {
        public object CorrelationContext 
        {
            get => Holder.Value?.Context;
            set
            {
                var holder = Holder.Value;
                if (holder != null)
                {
                    holder.Context = null;
                }

                if (value != null)
                {
                    Holder.Value = new CorrelationContextHolder { Context = value };
                }
            }
        }

        private static AsyncLocal<CorrelationContextHolder> Holder = new AsyncLocal<CorrelationContextHolder>();

        private class CorrelationContextHolder
        {
            public object Context;
        }
    }
}

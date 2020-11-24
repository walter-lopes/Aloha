using Microsoft.AspNetCore.Http;
using System;

namespace Aloha.Logging.Loggers
{
    public class LogEvent
    {
        public string TraceId { get; private set; }
        public string SpanId { get; private set; }
        public string ParentSpanId { get; private set; }
        public string CustomerId { get; private set; }

        public LogEvent() { }

        public LogEvent(Guid traceId, Guid spanId)
        {
            TraceId = traceId.ToString("N");
            SpanId = spanId.ToString("N").Substring(0, 16);
        }

        public void SetTraceContext(IHttpContextAccessor httpContextAccessor)
        {
            if (httpContextAccessor == null)
                return;

            SetTraceId(httpContextAccessor);
            SetSpanId();
            SetParentSpanId(httpContextAccessor);
            SetCustomerId(httpContextAccessor);

        }

        public void SetTraceId(IHttpContextAccessor httpContextAccessor)
        {
            string result = httpContextAccessor.HttpContext.Request.Headers["X-B3-TraceId"].ToString();
            TraceId = !string.IsNullOrEmpty(result) ? result : Guid.NewGuid().ToString("N");
        }

        public void SetSpanId()
        {
            SpanId = Guid.NewGuid().ToString("N").Substring(0, 16);
        }

        public void SetParentSpanId(IHttpContextAccessor httpContextAccessor)
        {
            ParentSpanId = httpContextAccessor.HttpContext.Request.Headers["X-B3-ParentSpanId"].ToString();
        }

        public void SetCustomerId(IHttpContextAccessor httpContextAccessor)
        {
            CustomerId = httpContextAccessor.HttpContext.Request.Headers["X-SM-CustomerId"].ToString();
        }
    }
}

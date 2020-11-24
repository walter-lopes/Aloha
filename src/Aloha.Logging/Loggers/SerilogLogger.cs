using Microsoft.AspNetCore.Http;
using System;
using System.Diagnostics;

namespace Aloha.Logging.Loggers
{
    public class SerilogLogger : ILogger
    {
        private Serilog.ILogger _logger;

        private readonly LogEvent _logEvent;

        private const string _messageTemplate = "{TraceId:l}, {ParentSpanId:l}, {SpanId:l}, {EventType:l}";

        public SerilogLogger(Serilog.ILogger logger, LogEvent logEvent)
        {
            _logger = logger;
            _logEvent = logEvent;
        }

        public void SetLoggerContext<T>()
        {
            _logger = _logger.ForContext<T>();
        }

        public void SetTraceContext(IHttpContextAccessor httpContextAccessor)
        {
            _logEvent.SetTraceContext(httpContextAccessor);
        }

        #region Debug

        public void DebugWithMessageTemplate(string messageTemplate, params object[] extraParams)
        {
            _logger.Debug(messageTemplate, extraParams);
        }

        public void Debug(string message, params object[] extraParams) => Debug(message, true, extraParams);

        public void Debug(string message, bool concatMethodName, params object[] extraParams)
        {
            if (concatMethodName)
                message = $"{GetParentMethodName()}-{message}";

            _logger.Debug(_messageTemplate, _logEvent.TraceId, _logEvent.ParentSpanId,
                _logEvent.SpanId, message, extraParams);
        }

        #endregion

        #region Information

        public void InformationWithMessageTemplate(string messageTemplate, params object[] extraParams)
        {
            _logger.Information(messageTemplate, extraParams);
        }

        public void Information(string message, params object[] extraParams) => Information(message, true, extraParams);

        public void Information(string message, bool concatMethodName, params object[] extraParams)
        {
            if (concatMethodName)
                message = $"{GetParentMethodName()}-{message}";


            _logger.Information(_messageTemplate, _logEvent.TraceId, _logEvent.ParentSpanId, _logEvent.SpanId, message, extraParams);

        }

        #endregion

        #region Warning

        public void WarningWithMessageTemplate(string messageTemplate, Exception exception = null, params object[] extraParams)
        {
            _logger.Warning(exception, messageTemplate, extraParams);
        }

        public void Warning(string message, Exception exception, params object[] extraParams) => Warning(message, exception, true, extraParams);

        public void Warning(string message, bool concatMethodName, params object[] extraParams) => Warning(message, null, concatMethodName, extraParams);

        public void Warning(string message, params object[] extraParams) => Warning(message, null, extraParams);

        public void Warning(string message, Exception exception, bool concatMethodName, params object[] extraParams)
        {
            if (concatMethodName)
                message = $"{GetParentMethodName()}-{message}";

            _logger.Warning(exception, _messageTemplate, _logEvent.TraceId, _logEvent.ParentSpanId,
                _logEvent.SpanId, message, extraParams);
        }

        #endregion

        #region Error

        public void ErrorWithMessageTemplate(string messageTemplate, Exception exception = null, params object[] extraParams)
        {
            _logger.Error(exception, messageTemplate, extraParams);
        }

        public void Error(string message, Exception exception, params object[] extraParams) => Error(message, exception, true, extraParams);

        public void Error(string message, bool concatMethodName, params object[] extraParams) => Error(message, null, concatMethodName, extraParams);

        public void Error(string message, params object[] extraParams) => Error(message, null, extraParams);

        public void Error(string message, Exception exception, bool concatMethodName, params object[] extraParams)
        {
            if (concatMethodName)
                message = $"{GetParentMethodName()}-{message}";

            _logger.Error(exception, _messageTemplate, _logEvent.TraceId, _logEvent.ParentSpanId,
                _logEvent.SpanId, message, extraParams);
        }

        #endregion

        private string GetParentMethodName()
        {
            StackTrace stackTrace = new StackTrace();
            return stackTrace.GetFrame(2).GetMethod().Name;
        }
    }
}

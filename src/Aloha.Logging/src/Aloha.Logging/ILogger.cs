using Microsoft.AspNetCore.Http;
using System;

namespace Aloha.Logging
{
    public interface ILogger
    {
        void SetLoggerContext<T>();

        void SetTraceContext(IHttpContextAccessor httpContextAccessor);

        void DebugWithMessageTemplate(string messageTemplate, params object[] extraParams);

        void Debug(string message, bool concatMethodName, params object[] extraParams);

        void Debug(string message, params object[] extraParams);

        void InformationWithMessageTemplate(string messageTemplate, params object[] extraParams);

        void Information(string message, bool concatMethodName, params object[] extraParams);

        void Information(string message, params object[] extraParams);

        void WarningWithMessageTemplate(string messageTemplate, Exception exception = null, params object[] extraParams);

        void Warning(string message, Exception exception, params object[] extraParams);

        void Warning(string message, bool concatMethodName, params object[] extraParams);

        void Warning(string message, params object[] extraParams);

        void Warning(string message, Exception exception, bool concatMethodName, params object[] extraParams);

        void ErrorWithMessageTemplate(string messageTemplate, Exception exception = null, params object[] extraParams);

        void Error(string message, Exception exception, params object[] extraParams);

        void Error(string message, bool concatMethodName, params object[] extraParams);

        void Error(string message, params object[] extraParams);

        void Error(string message, Exception exception, bool concatMethodName, params object[] extraParams);
    }
}

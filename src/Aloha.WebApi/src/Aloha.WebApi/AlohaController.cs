using Aloha.CQRS.Notifications;
using Aloha.CQRS.Notifications.Dispatchers;
using Aloha.Notifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Aloha.WebApi
{
    public class AlohaController : ControllerBase
    {
        protected readonly NotificationDispatcher _notificationDispatcher;

        public AlohaController(INotificationDispatcher notificationDispatcher)
        {
            _notificationDispatcher = (NotificationDispatcher)notificationDispatcher;
        }

        protected IEnumerable<DomainNotification> Notifications => _notificationDispatcher.GetNotifications();

        /// <summary>
        /// Check if has any notification.
        /// </summary>
        /// <returns></returns>
        protected bool IsValidOperation()
            => (!_notificationDispatcher.HasNotifications());

        protected void NotifyModelStateErrors()
        {
            var erros = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var erro in erros)
            {
                var erroMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                NotifyError(HttpStatusCode.BadRequest, erroMsg);
            }
        }

        /// <summary>
        /// Notify an error to notification 
        /// </summary>
        /// <param name="httpStatusCode"></param>
        /// <param name="message"></param>
        protected void NotifyError(HttpStatusCode httpStatusCode, string message)
            => _notificationDispatcher.PublishAsync(new DomainNotification(httpStatusCode, message));

        /// <summary>
        /// Notify bad request error messages to list of notification
        /// </summary>
        /// <param name="message">Error messages</param>
        protected async Task NotifyBadRequestErrorsAsync(IEnumerable<string> messages)
        {
            IEnumerable<DomainNotification> notifications = messages.Select(message => new DomainNotification(HttpStatusCode.BadRequest, message));

            await _notificationDispatcher.PublishAsync(notifications);
        }

        /// <summary>
        /// Response based on notification
        /// </summary>
        /// <param name="result">New action result</param>
        /// <returns></returns>
        protected new IActionResult Response(object result = null)
        {
            if (IsValidOperation())
            {
                return Ok(new
                {
                    success = true,
                    data = result
                });
            }

            return ApplyResponseBasedOnStatusCode();
        }

        /// <summary>
        /// Based on http status code, receive the action result correct.
        /// </summary>
        /// <returns></returns>
        private IActionResult ApplyResponseBasedOnStatusCode()
        {
            var statusCode = _notificationDispatcher.GetHttpStatusCode();

            return statusCode switch
            {
                HttpStatusCode.BadRequest => BadRequest(new
                {
                    success = false,
                    errors = _notificationDispatcher.GetNotifications().Select(n => n.Value)
                }),

                HttpStatusCode.NotFound => NotFound(new
                {
                    success = false,
                    errors = _notificationDispatcher.GetNotifications().Select(n => n.Value)
                }),

                HttpStatusCode.Conflict => Conflict(new
                {
                    success = false,
                    errors = _notificationDispatcher.GetNotifications().Select(n => n.Value)
                }),

                _ => StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    errors = _notificationDispatcher.GetNotifications().Select(n => n.Value)
                }),
            };
        }
    }
}

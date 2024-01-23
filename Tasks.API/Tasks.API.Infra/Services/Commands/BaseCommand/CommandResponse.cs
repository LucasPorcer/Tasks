using Tasks.API.Domain.Notifications;
using System.Collections.Generic;
using System.Net;

namespace Tasks.API.Application.Services.Commands.BaseCommand
{
    public class CommandResponse<T> : CommandResponse
    {
        public T Response { get; private set; }
        public IEnumerable<HttpStatusCode> MultiStatus { get; private set; }

        internal CommandResponse(bool isValid, T response) : base(isValid)
        {
            Response = response;
        }

        internal CommandResponse(DomainNotification validation, T response) : base(validation)
        {
            Response = response;
        }

        internal CommandResponse(IEnumerable<HttpStatusCode> multiStatus, DomainNotification validation, T response) : base(true, validation)
        {
            Response = response;
            MultiStatus = multiStatus;
        }
    }

    public class CommandResponse
    {
        public DomainNotification DomainNotification { get; private set; }

        public bool IsValid { get; private set; }

        protected CommandResponse(bool isValid = true)
        {
            IsValid = isValid;
        }

        protected CommandResponse(DomainNotification notification) : this(notification.IsValid)
        {
            DomainNotification = notification;
        }

        protected CommandResponse(bool isValid, DomainNotification notification) : this(isValid)
        {
            DomainNotification = notification;
        }

        public static CommandResponse BuildFailResponse()
        {
            return new CommandResponse(false);
        }

        public static CommandResponse BuildSuccessResponse()
        {
            return new CommandResponse();
        }

        public static CommandResponse BuildResponse(DomainNotification domainNotification)
        {
            return new CommandResponse(domainNotification);
        }

        public static CommandResponse<T> BuildResponse<T>(DomainNotification domainNotification, T response)
        {
            return new CommandResponse<T>(domainNotification, response);
        }

        public static CommandResponse<T> BuildResponse<T>(T response, bool isValid = true)
        {
            return new CommandResponse<T>(isValid, response);
        }

        public static CommandResponse<T> BuildMultiStatusResponse<T>(IEnumerable<HttpStatusCode> multiStatus, DomainNotification domainNotification, T response)
        {
            return new CommandResponse<T>(multiStatus, domainNotification, response);
        }
    }
}

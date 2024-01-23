using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Tasks.API.UI.Responses
{
    public class HttpErrorResponse : HttpResponse
    {
        public HttpErrorResponse(HttpStatusCode httpCode, IEnumerable<string> errors) : base(httpCode)
        {
            Success = false;
            Errors = errors ?? Enumerable.Empty<string>();
        }

        public IEnumerable<string> Errors { get; private set; }
    }
}

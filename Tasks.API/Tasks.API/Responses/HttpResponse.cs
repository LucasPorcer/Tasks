using System.Net;

namespace Tasks.API.UI.Responses
{
    public class HttpResponse
    {
        public HttpResponse(HttpStatusCode httpCode)
        {
            Success = true;
            HttpCode = (int)httpCode;
        }

        public HttpResponse(bool success, HttpStatusCode httpCode)
        {
            Success = success;
            HttpCode = (int)httpCode;
        }

        public bool Success { get; protected set; }
        public int HttpCode { get; protected set; }
    }
}

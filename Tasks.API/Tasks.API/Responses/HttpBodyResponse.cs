using System.Net;

namespace Tasks.API.UI.Responses
{
    public class HttpBodyResponse<T> : HttpResponse
    {
        public HttpBodyResponse(HttpStatusCode httpCode, T data) : base(httpCode)
        {
            Data = data;
        }

        public T Data { get; private set; }
    }
}

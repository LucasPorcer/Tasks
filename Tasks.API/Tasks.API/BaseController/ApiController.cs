using Microsoft.AspNetCore.Mvc;
using Tasks.API.UI.Responses;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Net;


namespace Tasks.API.UI.BaseController
{
    [ApiController]
    [SwaggerResponse(400, type: typeof(HttpErrorResponse))]
    [SwaggerResponse(401)]
    [SwaggerResponse(403)]
    [SwaggerResponse(500, type: typeof(HttpErrorResponse))]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class ApiController : ControllerBase
    {
        protected const string GROUP_NAME_V1 = "v1.0";
        protected const string GROUP_NAME_EXTERNAL = "external";
        protected const string API_VERSION_PATH = "v{version:apiVersion}/api/";
        protected const string API_PATH = "api/[controller]/[action]";
        protected const string BASE_PATH = "api/[controller]";

        [NonAction]
        public virtual string[] GetAllGroupNames()
        {
            return new[] { GROUP_NAME_V1, GROUP_NAME_EXTERNAL };
        }

        [NonAction]
        public ActionResult<HttpResponse> ApiResponse(HttpStatusCode httpStatusCode, string error)
          => ApiResponse(httpStatusCode, null, new List<string>() { error });

        [NonAction]
        public ActionResult<HttpResponse> ApiResponse(HttpStatusCode httpStatusCode, object data = null, IEnumerable<string> errors = null, IEnumerable<HttpStatusCode> multiStatus = null)
        {
            var response = CreateResponse(httpStatusCode, data, errors, multiStatus);

            switch (httpStatusCode)
            {
                case HttpStatusCode.NoContent:
                    return NoContent();
                case HttpStatusCode.OK:
                case HttpStatusCode.Created:
                    return Ok(response);
                case HttpStatusCode.MultiStatus:
                    return StatusCode((int)HttpStatusCode.MultiStatus, response);
                case HttpStatusCode.NotFound:
                    return NotFound(response);
                case HttpStatusCode.BadRequest:
                    return BadRequest(response);
                default:
                    return NoContent();
            }
        }

        [NonAction]
        public ActionResult<HttpResponse> ApiCreatedResponse(string routeName, object routeValues, object value = null)
        {
            return CreatedAtRoute(routeName, routeValues, value);
        }

        private HttpResponse CreateResponse(HttpStatusCode httpCode, object data = null, IEnumerable<string> errors = null, IEnumerable<HttpStatusCode> multiStatus = null)
        {
            switch (httpCode)
            {
                case HttpStatusCode.NoContent:
                case HttpStatusCode.OK:
                case HttpStatusCode.Created:
                    return data != null ? new HttpBodyResponse<object>(httpCode, data) : new HttpResponse(httpCode);               
                case HttpStatusCode.NotFound:
                case HttpStatusCode.BadRequest:
                default:
                    return new HttpErrorResponse(httpCode, errors);
            }
        }
    }
}

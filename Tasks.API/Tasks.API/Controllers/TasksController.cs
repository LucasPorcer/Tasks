using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tasks.API.UI.BaseController;
using Tasks.API.UI.Responses;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using System.Threading.Tasks;
using Tasks.API.Application.Services.Commands.Tasks.Command;
using System;
using Tasks.API.Application.Services.Commands.Tasks.Queries;

namespace Tasks.API.Controllers
{
    [Route("api/tasks")]
    [ApiController]
    public class TasksController : ApiController
    {
        private readonly IMediator _mediator;

        public TasksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Create a Task.
        /// </summary>
        /// <returns>Created Task</returns>
        /// <remarks>Created Task</remarks>
        /// <response code="200">Success: Returns the created Task.</response>
        /// <response code="400">Failure: Problem with the requisition.</response>
        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<ActionResult<HttpResponse>> Create([FromBody] CreateTaskCommand command)
        {
            var result = await _mediator.Send(command);
            return result.DomainNotification.IsValid
                ? ApiResponse(HttpStatusCode.Created, result)
                : ApiResponse(HttpStatusCode.BadRequest, errors: result.DomainNotification.Errors);
        }

        /// <summary>
        /// Remove a Task.
        /// </summary>
        /// <returns>Remove Task</returns>
        /// <remarks>Remove Task</remarks>
        /// <response code="200">Success: Ok</response>
        /// <response code="400">Failure: Problem with the requisition.</response>
        [HttpDelete("{taskId}")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<HttpResponse>> Remove([FromRoute] Guid taskId)
        {
            var command = new RemoveTaskCommand() { Id = taskId };

            var result = await _mediator.Send(command);
            return result.IsValid
                ? ApiResponse(HttpStatusCode.NoContent)
                : ApiResponse(HttpStatusCode.BadRequest);
        }

        /// <summary>
        /// Update a Task.
        /// </summary>
        /// <returns>Update Task</returns>
        /// <remarks>Update Task</remarks>
        /// <response code="200">Success: Ok</response>
        /// <response code="400">Failure: Problem with the requisition.</response>
        [HttpPut("{taskId}")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<HttpResponse>> Update([FromRoute] Guid taskId, [FromBody] UpdateTaskCommand command)
        {
            command.Id = taskId;

            var result = await _mediator.Send(command);
            return result.DomainNotification.IsValid
                ? ApiResponse(HttpStatusCode.OK)
                : ApiResponse(HttpStatusCode.BadRequest, errors: result.DomainNotification.Errors);
        }

        /// <summary>
        /// Get a Task.
        /// </summary>
        /// <returns>Get Task</returns>
        /// <remarks>Get Task</remarks>
        /// <response code="200">Success: Ok</response>
        /// <response code="400">Failure: Problem with the requisition.</response>
        [HttpGet("{taskId}")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<HttpResponse>> GetById([FromRoute] Guid taskId)
        {
            var query = new GetTaskByIdQuery() { Id = taskId }; 

            var result = await _mediator.Send(query);

            return result.IsValid
                ? ApiResponse(HttpStatusCode.OK, result)
                : ApiResponse(HttpStatusCode.NotFound);
        }

        /// <summary>
        /// Get all Tasks
        /// </summary>
        /// <returns>Get all Tasks</returns>
        /// <remarks>Get all Tasks</remarks>
        /// <response code="200">Success: Ok</response>
        /// <response code="400">Failure: Problem with the requisition.</response>
        [HttpGet()]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<HttpResponse>> GetAll()
        {
            var query = new GetAllTasksQuery();

            var result = await _mediator.Send(query);

            return result.IsValid
                ? ApiResponse(HttpStatusCode.OK, result)
                : ApiResponse(HttpStatusCode.NotFound);
        }
    }
}

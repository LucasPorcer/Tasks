using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tasks.API.Application.Services.Commands.BaseCommand;
using Tasks.API.Application.Services.Commands.Tasks.Queries;
using Tasks.API.Application.Services.Dto.Tasks;
using Tasks.API.Domain.Interfaces.Repository.Tasks;
using Tasks.API.Domain.Notifications;

namespace Tasks.API.Application.Services.Commands.Tasks
{
    public class TasksQueryHandler : IRequestHandler<GetAllTasksQuery, CommandResponse>,
        IRequestHandler<GetTaskByIdQuery, CommandResponse>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ILogger<TasksCommandHandler> _logger;
        private readonly DomainNotification _domainNotification;
        public TasksQueryHandler(ITaskRepository taskRepository,
             ILogger<TasksCommandHandler> logger,
             DomainNotification domainNotification)
        {
            _taskRepository = taskRepository;
            _logger = logger;
            _domainNotification = domainNotification;
        }

        public async Task<CommandResponse> Handle(GetAllTasksQuery query, CancellationToken cancellationToken)
        {
            try
            {
                var tasks = _taskRepository.GetAll();

                var tasksDto = tasks.Select(x => new TasksDto(
                    x.Id,
                    x.Name,
                    x.Description,
                    x.Done,
                    x.CreatedAt,
                    x.EndedAt,
                    x.UpdatedAt, 
                    x.Items));

                return CommandResponse.BuildResponse(_domainNotification, tasksDto);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return CommandResponse.BuildResponse(ex.Message, false);
            }
        }

        public async Task<CommandResponse> Handle(GetTaskByIdQuery query, CancellationToken cancellationToken)
        {
            try
            {
                var task = _taskRepository.GetById(query.Id);

                if (task == null)
                {
                    return CommandResponse.BuildFailResponse();
                }

                var taskDto = new TasksDto(
                    task.Id,
                    task.Name,
                    task.Description,
                    task.Done,
                    task.CreatedAt,
                    task.EndedAt,
                    task.UpdatedAt,
                    task.Items);

                return CommandResponse.BuildResponse(_domainNotification, taskDto);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return CommandResponse.BuildResponse(ex.Message, false);
            }
        }
    }
}

using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tasks.API.Application.Services.Commands.BaseCommand;
using Tasks.API.Application.Services.Commands.Tasks.Command;
using Tasks.API.Application.Services.Dto.Tasks;
using Tasks.API.Application.Services.Validators.Tasks;
using Tasks.API.Domain.Entities.Tasks;
using Tasks.API.Domain.Interfaces.Repository.Tasks;
using Tasks.API.Domain.Notifications;

namespace Tasks.API.Application.Services.Commands.Tasks
{

    public class TasksCommandHandler : IRequestHandler<CreateTaskCommand, CommandResponse>,
       IRequestHandler<RemoveTaskCommand, CommandResponse>,
       IRequestHandler<UpdateTaskCommand, CommandResponse>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ILogger<TasksCommandHandler> _logger;
        private readonly DomainNotification _domainNotification;
        public TasksCommandHandler(ITaskRepository taskRepository,
             ILogger<TasksCommandHandler> logger,
             DomainNotification domainNotification)
        {
            _taskRepository = taskRepository;
            _logger = logger;
            _domainNotification = domainNotification;
        }

        public async Task<CommandResponse> Handle(CreateTaskCommand command, CancellationToken cancellationToken)
        {
            try
            {
                _domainNotification.AddNotification(new CreateTaskCommandValidator().Validate(command));

                if (_domainNotification.IsValid)
                {
                    var task = new Domain.Entities.Tasks.Task(command.Name, command.Description, command.Done, DateTime.Now,
                    GenerateTaskItens(command.Items));

                    _taskRepository.Add(task);

                    var dto = new TasksDto(task.Id,
                        task.Name,
                        task.Description,
                        task.Done,
                        task.CreatedAt,
                        task.EndedAt,
                        task.UpdatedAt,
                        task.Items);

                    return CommandResponse.BuildResponse(_domainNotification, dto);
                }

                return CommandResponse.BuildResponse(_domainNotification);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return CommandResponse.BuildResponse(ex.Message, false);
            }
        }

        private List<TaskItem> GenerateTaskItens(List<CreateTaskItemCommand> items)
        {
            return items.Select(item => new TaskItem(item.Type, item.Data)).ToList();
        }

        public async Task<CommandResponse> Handle(RemoveTaskCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var task = _taskRepository.GetById(request.Id);
                
                if (task == null)
                {
                    _logger.LogWarning($"Task with id {request.Id} not found");
                    return CommandResponse.BuildResponse($"Task with id {request.Id} not found",false);
                }

                _taskRepository.Remove(task);

                return CommandResponse.BuildResponse(_domainNotification);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return CommandResponse.BuildResponse(ex.Message, false);
            }
        }

        public async Task<CommandResponse> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var task = _taskRepository.GetById(request.Id);

                if (task == null)
                {
                    _logger.LogWarning($"Task with id {request.Id} not found");

                    return CommandResponse.BuildResponse($"Task with id {request.Id} not found", false);
                }

                _domainNotification.AddNotification(new UpdateTaskCommandValidator().Validate(task));

                if (_domainNotification.IsValid)
                {
                    task.Update(request.Name, request.Description);
                    _taskRepository.Update(task);
                }

                return CommandResponse.BuildResponse(_domainNotification);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return CommandResponse.BuildResponse(ex.Message, false);
            }
        }
    }
}

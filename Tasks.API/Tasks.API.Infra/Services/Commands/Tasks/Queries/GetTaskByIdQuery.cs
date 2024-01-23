using System;
using Tasks.API.Application.Services.Commands.BaseCommand;

namespace Tasks.API.Application.Services.Commands.Tasks.Queries
{
    public class GetTaskByIdQuery : ICommand
    {
        public Guid Id { get; set; }
    }
}

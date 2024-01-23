using System;
using Tasks.API.Application.Services.Commands.BaseCommand;

namespace Tasks.API.Application.Services.Commands.Tasks.Command
{
    public class RemoveTaskCommand : ICommand
    {
        public Guid Id { get; set; }
    }
}

using System.Collections.Generic;
using Tasks.API.Application.Services.Commands.BaseCommand;
using Tasks.API.Domain.Enums;

namespace Tasks.API.Application.Services.Commands.Tasks.Command
{
    public class CreateTaskCommand : ICommand
    {
        public string Name { get;  set; }
        public string Description { get; set; }
        public bool Done { get; set; }
        public List<CreateTaskItemCommand> Items { get; set; }
    }

    public class CreateTaskItemCommand
    {  
        public TaskSubItem Type { get; set; }
        public string Data { get; set; }
    }
}

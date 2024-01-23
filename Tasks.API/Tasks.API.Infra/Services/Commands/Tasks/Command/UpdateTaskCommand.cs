using System;
using System.Text.Json.Serialization;
using Tasks.API.Application.Services.Commands.BaseCommand;

namespace Tasks.API.Application.Services.Commands.Tasks.Command
{
    public class UpdateTaskCommand : ICommand
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Done { get; set; }
    }
}

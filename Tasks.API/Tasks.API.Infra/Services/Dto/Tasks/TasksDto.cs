using System;
using System.Collections.Generic;
using Tasks.API.Domain.Entities.Tasks;

namespace Tasks.API.Application.Services.Dto.Tasks
{
    public record TasksDto
    {
        public TasksDto(Guid id, 
            string name,
            string description, 
            bool done,
            DateTime createdAt,
            DateTime? endedAt,
            DateTime? updatedAt,
            List<TaskItem> items)
        {
            Id = id;
            Name = name;
            Description = description;
            Done = done;
            CreatedAt = createdAt;
            EndedAt = endedAt;
            UpdatedAt = updatedAt;
            Items = items;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public bool Done { get; set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? EndedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        public List<TaskItem>? Items { get; set; }
    }
}

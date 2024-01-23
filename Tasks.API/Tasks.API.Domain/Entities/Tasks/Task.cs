using System;
using System.Collections.Generic;

namespace Tasks.API.Domain.Entities.Tasks
{
    public class Task
    {
        public Task(
            string name,
            string description, 
            bool done,
            DateTime createdAt,
            List<TaskItem>? items = null,
            DateTime? endedAt = null,
            DateTime? updatedAt = null)
        {
            Name = name;
            Description = description;
            Done = done;
            CreatedAt = createdAt;
            EndedAt = endedAt;
            UpdatedAt = updatedAt;
            Items = items;
        }

        public Task()
        {
        }

        public Guid Id { get;  set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public bool Done { get; set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? EndedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        public List<TaskItem>? Items { get; set; }

        public void Update(string name , string description)
        {
            UpdatedAt = DateTime.Now;
            Description = description;
            Name = name;
        }

        public void MarkAsDone()
        {
            EndedAt = DateTime.Now;
            Done = true;
        }
    }
}

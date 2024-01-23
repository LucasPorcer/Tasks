using System;
using Tasks.API.Domain.Enums;

namespace Tasks.API.Domain.Entities.Tasks
{
    public class TaskItem
    {
        public TaskItem()
        {
        }

        public TaskItem(
            TaskSubItem type,
            string data)
        {
            Type = type;
            Data = data;
        }

        public Guid Id { get; set; } 
        public TaskSubItem Type { get; private set; }
        public string Data { get; private set; }
    }
}

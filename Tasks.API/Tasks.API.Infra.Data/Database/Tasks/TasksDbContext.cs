using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Tasks.API.Domain.Entities.Tasks;
using Tasks.API.InfraData.Mappings.Tasks;

namespace Tasks.API.InfraData.Database.Tasks
{
    public class TasksDbContext : DbContext
    {
        public TasksDbContext(DbContextOptions<TasksDbContext> options) : base(options) { }

        public DbSet<Task> Tasks { get; set; }
        public DbSet<TaskItem> TaskItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("tasks_schema");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TaskMap).GetTypeInfo().Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TaskItemMap).GetTypeInfo().Assembly);
        }
    }
}

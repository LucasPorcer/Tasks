using System.Security.Cryptography.X509Certificates;
using Tasks.API.Domain.Entities.Tasks;
using Tasks.API.Domain.Interfaces.Repository.Tasks;
using Tasks.API.InfraData.Database.Tasks;

namespace Tasks.API.InfraData.Repositories.Tasks
{
    public class TaskRepository :  DatabaseRepositoryBase<Task>, ITaskRepository
    {
        public TaskRepository(TasksDbContext context) : base(context)
        {
             
        }
    }
}

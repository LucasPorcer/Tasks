using Moq;
using Xunit;
using Moq.AutoMock;
using FluentAssertions;
using Tasks.API.Application.Services.Commands.Tasks;
using Tasks.API.Application.Services.Commands.Tasks.Command;
using System.Collections.Generic;
using Tasks.API.Domain.Notifications;
using Tasks.API.Domain.Interfaces.Repository.Tasks;
using Tasks.API.Domain.Entities.Tasks;
using System.Linq;
using Task = Tasks.API.Domain.Entities.Tasks.Task;

namespace Tasks.API.Test.Unit.HandlerTest
{
    public class TasksrCommandHandlerTest : BaseUnitTests
    {
        private readonly AutoMocker _mocker;
        private TasksCommandHandler _handler;

        public TasksrCommandHandlerTest()
        {
            _mocker = new AutoMocker();
        }

        [Fact(DisplayName = "Create Task - Should Create a Task Correctly")]
        public async System.Threading.Tasks.Task ShouldCreateTaskCorrectly()
        {
            // Arrange
            var command = new CreateTaskCommand
            {
                Name = "Test",
                Description = "Description Test",
                Done = false,
                Items = new List<CreateTaskItemCommand> { new CreateTaskItemCommand { Data = string.Empty, Type = Domain.Enums.TaskSubItem.CheckBox } }
            };

            CreateHandleInstance();

            var taskFromDb = new Task(command.Name, command.Description, command.Done, System.DateTime.Now, GenerateTaskItens(command.Items));

            _mocker.GetMock<ITaskRepository>().Setup(p => p.Add(It.IsAny<Task>())).Returns(taskFromDb);

            // Act
            await _handler.Handle(command, new System.Threading.CancellationToken());

            // Assert
            _mocker.GetMock<DomainNotification>().Object.IsValid.Should().BeTrue();
        }

        #region PrivateMethods

        private void CreateHandleInstance() =>
            _handler = _mocker.CreateInstance<TasksCommandHandler>();

        private List<TaskItem> GenerateTaskItens(List<CreateTaskItemCommand> items)
        {
            return items.Select(item => new TaskItem(item.Type, item.Data)).ToList();
        }

        #endregion

    }
}

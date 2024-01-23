using FluentValidation;
using FluentValidation.Results;
using Tasks.API.Application.Services.Commands.Tasks.Command;
using Tasks.API.Domain.Entities.Tasks;

namespace Tasks.API.Application.Services.Validators.Tasks
{
    public class TaskValidators : AbstractValidator<Task>
    {
    }

    public class CreateTaskCommandValidator : AbstractValidator<CreateTaskCommand>
    {
        public CreateTaskCommandValidator()
        {
            RuleFor(c => c)
                .NotNull();

            RuleFor(c => c.Name).NotEmpty();

            RuleFor(c => c.Done).NotNull();
        }
    }

    public class UpdateTaskCommandValidator : AbstractValidator<Task>
    {
        public UpdateTaskCommandValidator()
        {

            RuleFor(c => c.Done)
                .NotEqual(true).WithMessage("Can't update done tasks");
        }

        public override ValidationResult Validate(ValidationContext<Task> context)
        {
            if (context.InstanceToValidate != null)
            {
                return base.Validate(context);
            }

            return new ValidationResult(new[] { new ValidationFailure("Task", "Task not found") });
        }
    }
}

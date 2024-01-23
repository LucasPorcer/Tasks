using MediatR;

namespace Tasks.API.Application.Services.Commands.BaseCommand
{
    public interface ICommand<T> : IRequest<CommandResponse<T>>
    {
    }

    public interface ICommand : IRequest<CommandResponse>
    {
    }
}

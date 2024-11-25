using ErrorOr;
using TimeTracker.Api.Application.Abstractions;
using TimeTracker.Api.Application.Errors;
using TimeTracker.Api.Domain.Tasks;

namespace TimeTracker.Api.Application.Tasks.DeleteTask;

public class DeleteTaskHandler(ITaskRepository taskRepository)
    : ICommandHandler<DeleteTaskCommand, Success>
{
    public async Task<ErrorOr<Success>> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await taskRepository.GetByIdAsync(request.Id, cancellationToken);
        if (task is null)
            return Error.NotFound(TaskErrors.NotFoundTitle, TaskErrors.NotFoundDescription);

        var result = await taskRepository.DeleteAsync(task, cancellationToken);
        
        return new Success { IsSuccess = result };
    }
}
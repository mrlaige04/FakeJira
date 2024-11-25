using ErrorOr;
using TimeTracker.Api.Application.Abstractions;
using TimeTracker.Api.Application.Errors;
using TimeTracker.Api.Domain.Tasks;

namespace TimeTracker.Api.Application.Tasks.GetTask;

public class GetTaskHandler(ITaskRepository taskRepository)
    : ICommandHandler<GetTaskQuery, GetTaskResponse>
{
    public async Task<ErrorOr<GetTaskResponse>> Handle(GetTaskQuery request, CancellationToken cancellationToken)
    {
        var task = await taskRepository.GetByIdAsync(request.Id, cancellationToken);
        
        if (task is null)
            return Error.NotFound(TaskErrors.NotFoundTitle, TaskErrors.NotFoundDescription);

        return new GetTaskResponse
        {
            Id = task.Id,
            Name = task.Name,
            Description = task.Description,
            Priority = task.Priority,
            AssigneeId = task.AssigneeId,
            Status = task.Status
        };
    }
}
using ErrorOr;
using TimeTracker.Api.Application.Abstractions;
using TimeTracker.Api.Application.Errors;
using TimeTracker.Api.Application.Tasks.GetTask;
using TimeTracker.Api.Domain.Tasks;

namespace TimeTracker.Api.Application.Tasks.UpdateTask;

public class UpdateTaskHandler(ITaskRepository taskRepository)
    : ICommandHandler<UpdateTaskCommand, GetTaskResponse>
{
    public async Task<ErrorOr<GetTaskResponse>> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await taskRepository.GetByIdAsync(request.Id, cancellationToken);
        if (task is null)
            return Error.NotFound(TaskErrors.NotFoundTitle, TaskErrors.NotFoundDescription);
        
        task = UpdateTask(task, request);
        
        var updatedTask = await taskRepository.UpdateAsync(task, cancellationToken);
        return new GetTaskResponse
        {
            Id = task.Id,
            Name = task.Name,
            Description = task.Description,
            Status = task.Status,
            AssigneeId = task.AssigneeId,
            Priority = task.Priority,
        };
    }

    private static ProjectTask UpdateTask(ProjectTask task, UpdateTaskCommand request)
    {
        task.Name = request.Name ?? task.Name;
        task.Description = request.Description ?? task.Description;
        task.Priority = request.Priority ?? task.Priority;
        task.AssigneeId = request.AssigneeId ?? task.AssigneeId;
        task.Status = request.Status ?? task.Status;
        return task;
    }
}
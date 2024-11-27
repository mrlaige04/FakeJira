using ErrorOr;
using MassTransit;
using RabbitMQ.Client.Exceptions;
using TimeTracker.Api.Application.Abstractions;
using TimeTracker.Api.Application.Errors;
using TimeTracker.Api.Application.RabbitMq;
using TimeTracker.Api.Application.Tasks.GetTask;
using TimeTracker.Api.Domain.Tasks;

namespace TimeTracker.Api.Application.Tasks.UpdateTask;

public class UpdateTaskHandler(
    ITaskRepository taskRepository,
    UserService userService)
    : ICommandHandler<UpdateTaskCommand, GetTaskResponse>
{
    public async Task<ErrorOr<GetTaskResponse>> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await taskRepository.GetByIdAsync(request.Id, cancellationToken);
        if (task is null)
            return Error.NotFound(TaskErrors.NotFoundTitle, TaskErrors.NotFoundDescription);
        
        task = await UpdateTask(task, request);
        
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

    private async Task<ProjectTask> UpdateTask(ProjectTask task, UpdateTaskCommand request)
    {
        task.Name = request.Name ?? task.Name;
        task.Description = request.Description ?? task.Description;
        task.Priority = request.Priority ?? task.Priority;
        task.Status = request.Status ?? task.Status;

        if (!request.AssigneeId.HasValue) return task;

        try
        {
            var user = await userService.GetUserAsync(request.AssigneeId.Value);
            if (user is not null)
                task.AssigneeId = request.AssigneeId.Value;
        }
        catch (BrokerUnreachableException) { }
        catch (RequestTimeoutException) { }

        return task;
    }
}
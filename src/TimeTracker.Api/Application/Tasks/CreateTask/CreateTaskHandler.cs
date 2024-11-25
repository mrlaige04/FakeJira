using ErrorOr;
using TimeTracker.Api.Application.Abstractions;
using TimeTracker.Api.Domain.Tasks;

namespace TimeTracker.Api.Application.Tasks.CreateTask;

public class CreateTaskHandler(ITaskRepository taskRepository)
    : ICommandHandler<CreateTaskCommand, CreateTaskResponse>
{
    public async Task<ErrorOr<CreateTaskResponse>> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        var task = new ProjectTask
        {
            Name = request.Name,
            Description = request.Description,
            Priority = request.Priority,
            AssigneeId = request.AssigneeId,
            Status = Status.ToDo
        };

        var createdTask = await taskRepository.CreateAsync(task, cancellationToken);

        return new CreateTaskResponse
        {
            Id = createdTask.Id,
            Name = createdTask.Name,
            Description = createdTask.Description,
            Priority = createdTask.Priority,
            AssigneeId = createdTask.AssigneeId,
            Status = createdTask.Status
        };
    }
}
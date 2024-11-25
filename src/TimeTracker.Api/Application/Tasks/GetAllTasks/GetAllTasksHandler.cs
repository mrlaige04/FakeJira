using ErrorOr;
using TimeTracker.Api.Application.Abstractions;
using TimeTracker.Api.Application.Tasks.GetTask;
using TimeTracker.Api.Domain.Tasks;

namespace TimeTracker.Api.Application.Tasks.GetAllTasks;

public class GetAllTasksHandler(ITaskRepository taskRepository)
    : ICommandHandler<GetAllTasksQuery, List<GetTaskResponse>>
{
    public async Task<ErrorOr<List<GetTaskResponse>>> Handle(GetAllTasksQuery request, CancellationToken cancellationToken)
    {
        var tasks = await taskRepository.GetAllAsync(cancellationToken);

        return tasks
            .Select(t => new GetTaskResponse
            {
                Id = t.Id,
                Name = t.Name,
                Description = t.Description,
                Priority = t.Priority,
                AssigneeId = t.AssigneeId,
                Status = t.Status
            }).ToList();
    }
}
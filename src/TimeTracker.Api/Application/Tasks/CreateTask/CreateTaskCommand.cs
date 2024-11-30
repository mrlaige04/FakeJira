using TimeTracker.Api.Application.Abstractions;
using TimeTracker.Api.Domain.Tasks;

namespace TimeTracker.Api.Application.Tasks.CreateTask;

public class CreateTaskCommand : ICommand<CreateTaskResponse>
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public Priority Priority { get; set; }
    public string AssigneeId { get; set; }
}
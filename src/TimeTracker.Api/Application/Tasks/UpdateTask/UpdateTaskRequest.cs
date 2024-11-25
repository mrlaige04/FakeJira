using TimeTracker.Api.Domain.Tasks;

namespace TimeTracker.Api.Application.Tasks.UpdateTask;

public class UpdateTaskRequest
{
    public string? Name { get; set; } = null!;
    public string? Description { get; set; } = null!;
    public Priority? Priority { get; set; } = null!;
    public int? AssigneeId { get; set; } = null!;
    public Status? Status { get; set; } = null!;
}
using TimeTracker.Api.Domain.Tasks;

namespace TimeTracker.Api.Application.Tasks.GetTask;

public class GetTaskResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public Priority Priority { get; set; }
    public string AssigneeId { get; set; }
    public Status Status { get; set; }
}
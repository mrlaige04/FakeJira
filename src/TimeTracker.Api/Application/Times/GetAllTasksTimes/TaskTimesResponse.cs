using TimeTracker.Api.Domain.Tasks;
using TimeTracker.Api.Domain.Times;

namespace TimeTracker.Api.Application.Times.GetAllTasksTimes;

public class TaskTimesResponse
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public Priority Priority { get; set; }
    public Status Status { get; set; }
    public int AssigneeId { get; set; }

    public ICollection<TimeLogResponse> TimeLogs { get; set; } = [];
}
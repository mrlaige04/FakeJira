using TimeTracker.Api.Domain.Abstractions;
using TimeTracker.Api.Domain.Tasks;

namespace TimeTracker.Api.Domain.Times;

public class TimeLog : BaseEntity
{
    public ProjectTask Task { get; set; } = null!;
    public int TaskId { get; set; }
    
    public DateOnly Date { get; set; }
    public string UserId { get; set; } = null!;
    
    public TimeOnly Start { get; set; }
    public TimeOnly End { get; set; }
}
using TimeTracker.Api.Domain.Abstractions;
using TimeTracker.Api.Domain.Tasks;

namespace TimeTracker.Api.Domain.Times;

public class TimeLog : BaseEntity
{
    public ProjectTask Task { get; set; } = null!;
    public int TaskId { get; set; }
    
    public DateTime Date { get; set; }
    public TimeSpan Time { get; set; }
    public int UserId { get; set; }
}
using TimeTracker.Api.Application.Abstractions;

namespace TimeTracker.Api.Application.Times.CreateTimeLog;

public class CreateTimeLogCommand : ICommand<TimeLogResponse>
{
    public int Id { get; set; }
    public string UserId { get; set; } = null!;
    public DateOnly Date { get; set; }
    public TimeOnly Start { get; set; }
    public TimeOnly End { get; set; }
}
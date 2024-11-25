namespace TimeTracker.Api.Application.Times.CreateTimeLog;

public class CreateTimeLogRequest
{
    public int UserId { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly Start { get; set; }
    public TimeOnly End { get; set; }
}
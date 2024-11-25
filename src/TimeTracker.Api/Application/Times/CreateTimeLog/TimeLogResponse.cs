namespace TimeTracker.Api.Application.Times.CreateTimeLog;

public class TimeLogResponse
{
    public int Id { get; set; }
    public int TaskId { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly Start { get; set; }
    public TimeOnly End { get; set; }
    public int UserId { get; set; }
}
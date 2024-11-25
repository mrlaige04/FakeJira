namespace TimeTracker.Api.Application.Times.GetAllTasksTimes;

public class TimeLogResponse
{
    public int Id { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly Start { get; set; }
    public TimeOnly End { get; set; }
    public int UserId { get; set; }
}
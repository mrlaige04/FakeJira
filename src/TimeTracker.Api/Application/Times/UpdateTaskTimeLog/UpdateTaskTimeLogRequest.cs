namespace TimeTracker.Api.Application.Times.UpdateTaskTimeLog;

public class UpdateTaskTimeLogRequest
{
    public DateOnly? Date { get; set; } = null;
    public TimeOnly? Start { get; set; } = null;
    public TimeOnly? End { get; set; } = null;
}
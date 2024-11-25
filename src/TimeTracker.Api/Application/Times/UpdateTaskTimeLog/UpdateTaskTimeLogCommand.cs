using TimeTracker.Api.Application.Abstractions;
using TimeTracker.Api.Application.Times.CreateTimeLog;

namespace TimeTracker.Api.Application.Times.UpdateTaskTimeLog;

public class UpdateTaskTimeLogCommand : ICommand<TimeLogResponse>
{
    public int TaskId { get; set; }
    public int LogId { get; set; }

    public DateOnly? Date { get; set; } = null;
    public TimeOnly? Start { get; set; } = null;
    public TimeOnly? End { get; set; } = null;
}
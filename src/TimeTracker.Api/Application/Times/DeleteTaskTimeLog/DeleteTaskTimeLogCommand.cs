using TimeTracker.Api.Application.Abstractions;

namespace TimeTracker.Api.Application.Times.DeleteTaskTimeLog;

public class DeleteTaskTimeLogCommand : ICommand<Success>
{
    public int TaskId { get; set; }
    public int LogId { get; set; }
}
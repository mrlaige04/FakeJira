using ErrorOr;
using Microsoft.EntityFrameworkCore;
using TimeTracker.Api.Application.Abstractions;
using TimeTracker.Api.Application.Errors;
using TimeTracker.Api.Application.Times.CreateTimeLog;
using TimeTracker.Api.Domain.Tasks;
using TimeTracker.Api.Domain.Times;

namespace TimeTracker.Api.Application.Times.UpdateTaskTimeLog;

public class UpdateTaskTimeLogHandler(ITaskRepository taskRepository)
    : ICommandHandler<UpdateTaskTimeLogCommand, TimeLogResponse>
{
    public async Task<ErrorOr<TimeLogResponse>> Handle(UpdateTaskTimeLogCommand request, CancellationToken cancellationToken)
    {
        var queryable = taskRepository.GetQuery();
        var task = await queryable
            .Include(t => t.TimeLogs)
            .Where(t => t.Id == request.TaskId)
            .FirstOrDefaultAsync(cancellationToken);
        
        if (task is null)
            return Error.NotFound(TaskErrors.NotFoundTitle, TaskErrors.NotFoundDescription);
        
        var timeLog = task.TimeLogs.FirstOrDefault(l => l.Id == request.LogId);
        if (timeLog is null)
            return Error.NotFound(TaskErrors.NotFoundTitle, TaskErrors.NotFoundDescription);
        
        timeLog = UpdateTimeLog(timeLog, request);
        
        await taskRepository.UpdateAsync(task, cancellationToken);

        return new TimeLogResponse
        {
            Id = timeLog.Id,
            Date = timeLog.Date,
            Start = timeLog.Start,
            End = timeLog.End,
            UserId = timeLog.UserId,
            TaskId = task.Id,
        };
    }

    private static TimeLog UpdateTimeLog(TimeLog timeLog, UpdateTaskTimeLogCommand request)
    {
        timeLog.Date = request.Date ?? timeLog.Date;
        
        timeLog.Start = request.Start ?? timeLog.Start;
        timeLog.End = request.End ?? timeLog.End;
        
        return timeLog;
    }
}
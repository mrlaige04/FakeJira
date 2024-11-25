using ErrorOr;
using Microsoft.EntityFrameworkCore;
using TimeTracker.Api.Application.Abstractions;
using TimeTracker.Api.Application.Errors;
using TimeTracker.Api.Domain.Tasks;
using TimeTracker.Api.Domain.Times;

namespace TimeTracker.Api.Application.Times.CreateTimeLog;

public class CreateTimeLogHandler(ITaskRepository taskRepository)
    : ICommandHandler<CreateTimeLogCommand, TimeLogResponse>
{
    public async Task<ErrorOr<TimeLogResponse>> Handle(CreateTimeLogCommand request, CancellationToken cancellationToken)
    {
        var queryable = taskRepository.GetQuery();
        var task = await queryable
            .Include(t => t.TimeLogs)
            .Where(t => t.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
        
        if (task is null)
            return Error.NotFound(TaskErrors.NotFoundTitle, TaskErrors.NotFoundDescription);

        var log = new TimeLog
        {
            Date = request.Date,
            UserId = request.UserId,
            Start = request.Start,
            End = request.End
        };
        
        task.AddLog(log);
        
        var updatedTask = await taskRepository.UpdateAsync(task, cancellationToken);
        var newLog = updatedTask.TimeLogs.LastOrDefault();

        return new TimeLogResponse
        {
            Id = newLog!.Id,
            Date = log.Date,
            Start = log.Start,
            End = log.End,
            TaskId = updatedTask.Id,
        };
    }
}
using ErrorOr;
using Microsoft.EntityFrameworkCore;
using TimeTracker.Api.Application.Abstractions;
using TimeTracker.Api.Application.Errors;
using TimeTracker.Api.Domain.Tasks;

namespace TimeTracker.Api.Application.Times.DeleteTaskTimeLog;

public class DeleteTaskTimeLogHandler(ITaskRepository taskRepository)
    : ICommandHandler<DeleteTaskTimeLogCommand, Success>
{
    public async Task<ErrorOr<Success>> Handle(DeleteTaskTimeLogCommand request, CancellationToken cancellationToken)
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
        
        task.RemoveLog(timeLog);
        
        await taskRepository.UpdateAsync(task, cancellationToken);

        return new Success { IsSuccess = true };
    }
}
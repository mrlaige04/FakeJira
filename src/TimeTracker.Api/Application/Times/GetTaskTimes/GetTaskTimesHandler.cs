using ErrorOr;
using Microsoft.EntityFrameworkCore;
using TimeTracker.Api.Application.Abstractions;
using TimeTracker.Api.Application.Errors;
using TimeTracker.Api.Application.Times.GetAllTasksTimes;
using TimeTracker.Api.Domain.Tasks;

namespace TimeTracker.Api.Application.Times.GetTaskTimes;

public class GetTaskTimesHandler(ITaskRepository taskRepository)
    : ICommandHandler<GetTaskTimesQuery, TaskTimesResponse>
{
    public async Task<ErrorOr<TaskTimesResponse>> Handle(GetTaskTimesQuery request, CancellationToken cancellationToken)
    {
        var queryable = taskRepository.GetQuery();

        var task = await queryable
            .Include(t => t.TimeLogs)
            .Where(t => t.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
        
        if (task is null)
            return Error.NotFound(TaskErrors.NotFoundTitle, TaskErrors.NotFoundDescription);

        var result = new TaskTimesResponse
        {
            Name = task.Name,
            Description = task.Description,
            Priority = task.Priority,
            Status = task.Status,
            AssigneeId = task.AssigneeId,
            TimeLogs = task.TimeLogs.Select(log => new TimeLogResponse
            {
                Id = log.Id,
                Date = log.Date,
                Start = log.Start,
                End = log.End,
                UserId = log.UserId,
            }).ToList()
        };

        return result;
    }
}
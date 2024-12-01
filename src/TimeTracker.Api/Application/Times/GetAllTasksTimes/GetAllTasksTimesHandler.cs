using ErrorOr;
using Microsoft.EntityFrameworkCore;
using TimeTracker.Api.Application.Abstractions;
using TimeTracker.Api.Domain.Tasks;

namespace TimeTracker.Api.Application.Times.GetAllTasksTimes;

public class GetAllTasksTimesHandler(ITaskRepository taskRepository)
    : ICommandHandler<GetAllTasksTimesQuery, List<TaskTimesResponse>>
{
    public async Task<ErrorOr<List<TaskTimesResponse>>> Handle(GetAllTasksTimesQuery request, CancellationToken cancellationToken)
    {
        var queryable = taskRepository.GetQuery();

        var tasks = await queryable
            .Include(t => t.TimeLogs)
            .Select(t => new TaskTimesResponse
            {
                TaskId = t.Id,
                Name = t.Name,
                Description = t.Description,
                Priority = t.Priority,
                Status = t.Status,
                AssigneeId = t.AssigneeId,
                TimeLogs = t.TimeLogs.Select(log => new TimeLogResponse
                {
                    Id = log.Id,
                    Date = log.Date,
                    Start = log.Start,
                    End = log.End,
                    UserId = log.UserId,
                }).ToList()
            })
            .ToListAsync(cancellationToken);

        return tasks;
    }
}
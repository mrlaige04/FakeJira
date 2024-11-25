using TimeTracker.Api.Application.Abstractions;
using TimeTracker.Api.Application.Tasks.GetTask;
using TimeTracker.Api.Domain.Tasks;

namespace TimeTracker.Api.Application.Tasks.UpdateTask;

public class UpdateTaskCommand : ICommand<GetTaskResponse>
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public Priority? Priority { get; set; }
    public int? AssigneeId { get; set; }
    public Status? Status { get; set; }
}
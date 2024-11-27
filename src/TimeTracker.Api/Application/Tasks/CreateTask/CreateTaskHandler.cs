using System.Net;
using ErrorOr;
using MassTransit;
using RabbitMQ.Client.Exceptions;
using TimeTracker.Api.Application.Abstractions;
using TimeTracker.Api.Application.RabbitMq;
using TimeTracker.Api.Domain.Tasks;

namespace TimeTracker.Api.Application.Tasks.CreateTask;

public class CreateTaskHandler(
    ITaskRepository taskRepository,
    UserService userService)
    : ICommandHandler<CreateTaskCommand, CreateTaskResponse>
{
    public async Task<ErrorOr<CreateTaskResponse>> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await userService.GetUserAsync(request.AssigneeId);
            if (user is null)
                return Error.Failure($"User {request.AssigneeId} does not exist");
        }
        catch (Exception)
        {
            return Error.Custom(StatusCodes.Status503ServiceUnavailable, "Api.Unreachable", "Services temporarily unavailable");
        }
        
        var task = new ProjectTask
        {
            Name = request.Name,
            Description = request.Description,
            Priority = request.Priority,
            AssigneeId = request.AssigneeId,
            Status = Status.ToDo
        };

        var createdTask = await taskRepository.CreateAsync(task, cancellationToken);

        return new CreateTaskResponse
        {
            Id = createdTask.Id,
            Name = createdTask.Name,
            Description = createdTask.Description,
            Priority = createdTask.Priority,
            AssigneeId = createdTask.AssigneeId,
            Status = createdTask.Status
        };
    }
}
using TimeTracker.Api.Application.Abstractions;

namespace TimeTracker.Api.Application.Tasks.GetTask;

public record GetTaskQuery(int Id) : ICommand<GetTaskResponse>;
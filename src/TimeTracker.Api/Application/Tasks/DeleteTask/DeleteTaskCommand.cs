using TimeTracker.Api.Application.Abstractions;

namespace TimeTracker.Api.Application.Tasks.DeleteTask;

public record DeleteTaskCommand(int Id) : ICommand<Success>;
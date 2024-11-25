using TimeTracker.Api.Application.Abstractions;
using TimeTracker.Api.Application.Times.GetAllTasksTimes;

namespace TimeTracker.Api.Application.Times.GetTaskTimes;

public record GetTaskTimesQuery(int Id) : ICommand<TaskTimesResponse>;
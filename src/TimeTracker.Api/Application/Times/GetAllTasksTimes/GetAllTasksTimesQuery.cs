using TimeTracker.Api.Application.Abstractions;

namespace TimeTracker.Api.Application.Times.GetAllTasksTimes;

public class GetAllTasksTimesQuery : ICommand<List<TaskTimesResponse>>;
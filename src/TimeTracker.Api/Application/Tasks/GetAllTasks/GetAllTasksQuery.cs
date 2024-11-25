using TimeTracker.Api.Application.Abstractions;
using TimeTracker.Api.Application.Tasks.GetTask;

namespace TimeTracker.Api.Application.Tasks.GetAllTasks;

public class GetAllTasksQuery : ICommand<List<GetTaskResponse>>;
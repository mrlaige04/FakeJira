using MediatR;
using Microsoft.AspNetCore.Mvc;
using TimeTracker.Api.Application.Tasks.CreateTask;
using TimeTracker.Api.Application.Tasks.DeleteTask;
using TimeTracker.Api.Application.Tasks.GetAllTasks;
using TimeTracker.Api.Application.Tasks.GetTask;
using TimeTracker.Api.Application.Tasks.UpdateTask;

namespace TimeTracker.Api.Controllers;

[Route("tasks")]
public class TaskController(ISender sender) : BaseController
{
    [HttpGet, Route("{id:int}")]
    public async Task<IActionResult> GetTask(int id)
    {
        var query = new GetTaskQuery(id);
        var result = await sender.Send(query);
        
        return result.Match<IActionResult>(Ok, NotFound);
    }

    [HttpGet]
    public async Task<IActionResult> GetTasks()
    {
        var query = new GetAllTasksQuery();
        var result = await sender.Send(query);
        return result.Match<IActionResult>(Ok, NotFound);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTask(CreateTaskCommand command)
    {
        var result = await sender.Send(command);
        return result.Match(Ok, ErrorsToResult);
    }

    [HttpPatch, Route("{id:int}")]
    public async Task<IActionResult> UpdateTask(int id, UpdateTaskRequest request)
    {
        var command = new UpdateTaskCommand()
        {
            Id = id,
            Name = request.Name,
            Description = request.Description,
            Status = request.Status,
            AssigneeId = request.AssigneeId,
            Priority = request.Priority,
        };
        
        var result = await sender.Send(command);
        return result.Match(Ok, ErrorsToResult);
    }

    [HttpDelete, Route("{id:int}")]
    public async Task<IActionResult> DeleteTask(int id)
    {
        var command = new DeleteTaskCommand(id);
        var result = await sender.Send(command);
        return result.Match<IActionResult>(Ok, NotFound);
    }
}
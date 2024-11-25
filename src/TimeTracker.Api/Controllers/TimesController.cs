using MediatR;
using Microsoft.AspNetCore.Mvc;
using TimeTracker.Api.Application.Times.CreateTimeLog;
using TimeTracker.Api.Application.Times.DeleteTaskTimeLog;
using TimeTracker.Api.Application.Times.GetAllTasksTimes;
using TimeTracker.Api.Application.Times.GetTaskTimes;
using TimeTracker.Api.Application.Times.UpdateTaskTimeLog;

namespace TimeTracker.Api.Controllers;

[Route("tasks")]
public class TimesController(ISender sender) : BaseController
{
    [HttpGet, Route("times")]
    public async Task<IActionResult> GetAllTaskTimes()
    {
        var query = new GetAllTasksTimesQuery();
        var result = await sender.Send(query);
        return result.Match(Ok, ErrorsToResult);
    }

    [HttpGet, Route("{id:int}/times")]
    public async Task<IActionResult> GetTaskTimes(int id)
    {
        var query = new GetTaskTimesQuery(id);
        var result = await sender.Send(query);
        return result.Match(Ok, ErrorsToResult);
    }

    [HttpPost, Route("{id:int}/times")]
    public async Task<IActionResult> AddTaskTime(int id, CreateTimeLogRequest request)
    {
        var command = new CreateTimeLogCommand
        {
            Id = id,
            Date = request.Date,
            Start = request.Start,
            End = request.End,
            UserId = request.UserId,
        };
        var result = await sender.Send(command);
        return result.Match(Ok, ErrorsToResult);
    }

    [HttpPatch, Route("{id:int}/times/{logId:int}")]
    public async Task<IActionResult> UpdateTaskTime(int id, int logId, UpdateTaskTimeLogRequest request)
    {
        var command = new UpdateTaskTimeLogCommand
        {
            Date = request.Date,
            TaskId = id,
            LogId = logId,
            Start = request.Start,
            End = request.End
        };
        
        var result = await sender.Send(command);
        return result.Match(Ok, ErrorsToResult);
    } 

    [HttpDelete, Route("{id:int}/times/{logId:int}")]
    public async Task<IActionResult> DeleteTaskTime(int id, int logId)
    {
        var command = new DeleteTaskTimeLogCommand
        {
            TaskId = id,
            LogId = logId
        };
        
        var result = await sender.Send(command);
        return result.Match(Ok, ErrorsToResult);
    }
}
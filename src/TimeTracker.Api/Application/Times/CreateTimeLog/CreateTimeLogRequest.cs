﻿namespace TimeTracker.Api.Application.Times.CreateTimeLog;

public class CreateTimeLogRequest
{
    public string UserId { get; set; } = null!;
    public DateOnly Date { get; set; }
    public TimeOnly Start { get; set; }
    public TimeOnly End { get; set; }
}
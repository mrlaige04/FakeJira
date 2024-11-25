﻿using TimeTracker.Api.Domain.Abstractions;
using TimeTracker.Api.Domain.Times;

namespace TimeTracker.Api.Domain.Tasks;

public class ProjectTask : BaseEntity
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public Priority Priority { get; set; }
    public Status Status { get; set; }
    public int AssigneeId { get; set; }

    public ICollection<TimeLog> TimeLogs { get; set; } = [];
}
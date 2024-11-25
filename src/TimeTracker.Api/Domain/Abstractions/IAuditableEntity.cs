﻿namespace TimeTracker.Api.Domain.Abstractions;

public interface IAuditableEntity
{
    DateTime CreatedAt { get; set; }
    DateTime? UpdatedAt { get; set; }
}
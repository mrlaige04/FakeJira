namespace TimeTracker.Api.Domain.Abstractions;

public abstract class BaseEntity : IEntity<int>, IAuditableEntity
{
    public int Id { get; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
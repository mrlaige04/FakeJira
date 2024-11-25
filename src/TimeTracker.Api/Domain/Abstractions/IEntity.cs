namespace TimeTracker.Api.Domain.Abstractions;

public interface IEntity<out TKey>
{
    TKey Id { get; }
}
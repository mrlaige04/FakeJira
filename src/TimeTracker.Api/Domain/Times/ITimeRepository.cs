namespace TimeTracker.Api.Domain.Times;

public interface ITimeRepository
{
    Task<TimeLog?> GetAsync(int id, CancellationToken cancellationToken = default);
    Task<List<TimeLog>> GetAllAsync(int? taskId = null, CancellationToken cancellationToken = default);
    Task<TimeLog> AddAsync(TimeLog timeLog, CancellationToken cancellationToken = default);
    Task<TimeLog> UpdateAsync(TimeLog timeLog, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
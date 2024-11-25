namespace TimeTracker.Api.Domain.Tasks;

public interface ITaskRepository
{
    Task<ProjectTask?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<List<ProjectTask>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<ProjectTask> CreateAsync(ProjectTask projectTask, CancellationToken cancellationToken = default);
    Task<ProjectTask> UpdateAsync(ProjectTask projectTask, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(ProjectTask task, CancellationToken cancellationToken = default);
    
    IQueryable<ProjectTask> GetQuery();
}
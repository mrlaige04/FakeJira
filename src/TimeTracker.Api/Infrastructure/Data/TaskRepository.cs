using Microsoft.EntityFrameworkCore;
using TimeTracker.Api.Domain.Tasks;

namespace TimeTracker.Api.Infrastructure.Data;

public class TaskRepository(AppDbContext context) : ITaskRepository
{
    public async Task<ProjectTask?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await context.Tasks
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<List<ProjectTask>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.Tasks
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<ProjectTask> CreateAsync(ProjectTask projectTask, CancellationToken cancellationToken = default)
    {
        var entry = await context.Tasks.AddAsync(projectTask, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return entry.Entity;
    }

    public async Task<ProjectTask> UpdateAsync(ProjectTask projectTask, CancellationToken cancellationToken = default)
    {
        context.Entry(projectTask).CurrentValues.SetValues(projectTask);
        await context.SaveChangesAsync(cancellationToken);
        return projectTask;
    }

    public async Task<bool> DeleteAsync(ProjectTask task, CancellationToken cancellationToken = default)
    {
        context.Remove(task);
        return await context.SaveChangesAsync(cancellationToken) > 0;
    }
}
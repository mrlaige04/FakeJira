using Microsoft.EntityFrameworkCore;
using TimeTracker.Api.Domain.Tasks;
using TimeTracker.Api.Domain.Times;

namespace TimeTracker.Api.Infrastructure.Data;

public sealed class AppDbContext : DbContext
{
    public DbSet<ProjectTask> Tasks { get; set; } = null!;
    public DbSet<TimeLog> TimeLogs { get; set; } = null!;
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<ProjectTask>()
            .HasKey(x => x.Id);
        
        modelBuilder.Entity<ProjectTask>()
            .HasIndex(x => x.Name);
        
        modelBuilder.Entity<ProjectTask>()
            .HasMany(t => t.TimeLogs)
            .WithOne(l => l.Task)
            .HasForeignKey(l => l.TaskId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<TimeLog>()
            .HasKey(x => x.Id);
    }
}
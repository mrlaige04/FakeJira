using System.Reflection;
using Microsoft.EntityFrameworkCore;
using TimeTracker.Api.Domain.Tasks;
using TimeTracker.Api.Infrastructure.Data;

namespace TimeTracker.Api;

public static class RegisterServices
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });
    }

    public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<AppDbContext>(opt =>
        {
            opt.UseSqlServer(connectionString);
        });

        services.AddScoped<ITaskRepository, TaskRepository>();
    }
}
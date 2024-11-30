using System.Reflection;
using Contracts.Configuration;
using Contracts.Users.GetUser;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using TimeTracker.Api.Application.RabbitMq;
using TimeTracker.Api.Application.RabbitMq.Consumers;
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

        services.AddMassTransit(x =>
        {
            x.AddRequestClient<GetUserRequest>(timeout: TimeSpan.FromSeconds(8));

            x.AddConsumer<DeleteUserConsumer>();
            
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(configuration["RabbitMq:Host"]!, h =>
                {
                    h.Username(configuration["RabbitMq:Username"]!);
                    h.Password(configuration["RabbitMq:Password"]!);
                });
                
                cfg.ConfigureEndpoints(context);
            });
        });
        
        services.AddScoped<UserService>();
    }
}
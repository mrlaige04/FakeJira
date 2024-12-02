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
        
        var rabbitMqConfig = configuration.GetSection("RabbitMq");
        services.AddMassTransit(x =>
        {
            x.AddRequestClient<GetUserResponse>(timeout: TimeSpan.FromSeconds(8));

            x.AddConsumer<DeleteUserConsumer>();
            
            x.UsingRabbitMq((context, cfg) =>
            {
                var host = rabbitMqConfig["Host"];
                var username = rabbitMqConfig["Username"];
                var password = rabbitMqConfig["Password"];

                cfg.Host(new Uri($"rabbitmq://{host}"), h =>
                {
                    h.Username(username!);
                    h.Password(password!);
                });
                
                cfg.ConfigureEndpoints(context);
            });
        });
        
        services.AddScoped<UserService>();
    }
}
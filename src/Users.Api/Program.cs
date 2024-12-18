using Contracts.Configuration;
using Contracts.Users.GetUser;
using MassTransit;
using MongoDB.Driver;
using Users.Api.RabbitMQ.Consumers;
using Users.Api.Services;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var connectionString = configuration["MongoDb__ConnectionString"]
                           ?? throw new InvalidOperationException("MongoDb connection string not found.");
    return new MongoClient(connectionString);
});

builder.Services.AddSingleton(sp =>
{
    var client = sp.GetRequiredService<IMongoClient>();
    var databaseName = configuration["MongoDb__DatabaseName"]
                       ?? throw new InvalidOperationException("MongoDb database name not found.");
    return client.GetDatabase(databaseName);
});

builder.Services.AddScoped<IUsersService, UsersService>();

var rabbitMqConfig = configuration.GetSection("RabbitMq");
builder.Services.AddMassTransit(x =>
{
    x.AddRequestClient<GetUserRequest>(timeout: TimeSpan.FromSeconds(8));

    x.AddConsumer<UpdateConsumer>();

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

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
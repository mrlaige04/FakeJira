using MongoDB.Driver;
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

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
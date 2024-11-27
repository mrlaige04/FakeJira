namespace Contracts.Configuration;

public class RabbitMqConfiguration
{
    public static string SectionName = "RabbitMq";
    public string Host { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
}
using Microsoft.EntityFrameworkCore;
namespace DataLayer;

public class AnnualEventsContext : DbContext
{
    public string HostName { get; set; }

    public string Port { get; set; }

    public string ServiceName { get; set; }

    public string UserName { get; set; }

    public string Password { get; set; }
    public AnnualEventsContext()
    {
        HostName = Environment.GetEnvironmentVariable("ORACLE_DB_HOST")!;

        Port = Environment.GetEnvironmentVariable("ORACLE_DB_PORT") ?? "1521";

        ServiceName = Environment.GetEnvironmentVariable("ORACLE_DB_SERVICE")!;

        UserName = Environment.GetEnvironmentVariable("ORACLE_DB_USER")!;

        Password = Environment.GetEnvironmentVariable("ORACLE_DB_PASSWORD")!;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        Console.WriteLine($"Data Source={HostName}:{Port}/{ServiceName}; " +
          $"User Id={UserName}");
        optionsBuilder.UseOracle($"Data Source={HostName}:{Port}/{ServiceName}; " +
          $"User Id={UserName}; Password={Password}");
    }
}
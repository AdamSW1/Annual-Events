using Microsoft.EntityFrameworkCore;
using BusinessLayer;
using RecipeInfo;
namespace DataLayer;

public class AnnualEventsContext : DbContext
{

    private static AnnualEventsContext? _instance;

    public static AnnualEventsContext Instance
    {
        get{
            _instance ??= new AnnualEventsContext();
            return _instance;
        }
    }
    
    public string DbPath { get; }
    public DbSet<Review> Review { get; set; }
    public DbSet<Annual_Events_User> Annual_Events_User { get; set; }
    public DbSet<Recipe> Recipe { get; set; }
    public DbSet<Preparation> Preparation { get; set; }
    public string HostName { get; set; }

    public string Port { get; set; }

    public string ServiceName { get; set; }

    public string UserName { get; set; }

    public string Password { get; set; }

    public AnnualEventsContext()
    {
        var path = "Annual-Events";
        DbPath = System.IO.Path.Join(path, "Annual-Events.db");
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
protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Recipe>()
                .HasOne(recipe => recipe.Owner)
                .WithMany(user => user.Recipes);

        modelBuilder.Entity<Annual_Events_User>()
            .HasMany(user => user.Recipes)
            .WithOne(recipe => recipe.Owner);

        modelBuilder.Entity<Recipe>()
               .HasMany(recipe => recipe.FavouritedBy)
               .WithMany(user => user.FavRecipes);

        modelBuilder.Entity<Annual_Events_User>()
            .HasMany(user => user.FavRecipes)
            .WithMany(recipe => recipe.FavouritedBy);
    }
}
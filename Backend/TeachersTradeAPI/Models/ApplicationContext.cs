using Microsoft.EntityFrameworkCore;

namespace TeachersTradeAPI.Models;

public sealed class ApplicationContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Teacher> Teachers { get; set; } = null!;
    public DbSet<Share> Shares { get; set; } = null!;

    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
        if (Database.CanConnect()) return;
        Console.WriteLine("Database connection failed, check your connection properties");
        throw new Exception("Database connection failed");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false).Build();
        optionsBuilder.UseNpgsql($"Host={configuration["DatabaseConnectionParams:Host"]};" +
                                 $"Port={configuration["DatabaseConnectionParams:Port"]};" +
                                 $"Database={configuration["DatabaseConnectionParams:Database"]};" +
                                 $"Username={configuration["DatabaseConnectionParams:Username"]};" +
                                 $"Password={configuration["DatabaseConnectionParams:Password"]}");
    }
}
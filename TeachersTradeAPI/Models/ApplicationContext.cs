using Microsoft.EntityFrameworkCore;

namespace TeachersTradeAPI.Models;

public sealed class ApplicationContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false).Build();
        optionsBuilder.UseNpgsql($"Host={configuration["DatabaseConnectionString:Host"]};" +
                                 $"Port={configuration["DatabaseConnectionString:Port"]};" +
                                 $"Database={configuration["DatabaseConnectionString:Database"]};" +
                                 $"Username={configuration["DatabaseConnectionString:Username"]};" +
                                 $"Password={configuration["DatabaseConnectionString:Password"]}");
    }
}
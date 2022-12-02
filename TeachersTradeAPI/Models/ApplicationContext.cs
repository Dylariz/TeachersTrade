using Microsoft.EntityFrameworkCore;

namespace TeachersTradeAPI.Models;

public sealed class ApplicationContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Teacher> Teachers { get; set; } = null!;

    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
        try
        {
            Database.CanConnect();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
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
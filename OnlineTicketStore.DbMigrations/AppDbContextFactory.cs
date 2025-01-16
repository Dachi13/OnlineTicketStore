using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace OnlineTicketStore.DbMigrations;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var builder = new DbContextOptionsBuilder<AppDbContext>();
        builder.UseNpgsql(configuration.GetConnectionString("Database"),
            b => b.MigrationsAssembly("OnlineTicketStore.DbMigrations"));

        return new AppDbContext(builder.Options);
    }
}
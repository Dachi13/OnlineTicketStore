using Microsoft.EntityFrameworkCore;

namespace OnlineTicketStore.DbMigrations;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}
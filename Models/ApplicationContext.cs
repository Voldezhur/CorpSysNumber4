using Microsoft.EntityFrameworkCore;
using Practice4.Models;

public class ApplicationContext : DbContext
{
    public DbSet<User> users { get; set; }
    public DbSet<Message> message { get; set; }

    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
}
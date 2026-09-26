using Microsoft.EntityFrameworkCore;
using MovieTicket.Model;

namespace MovieTicket.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
        
    }
    public DbSet<User> Users {get;set;}
}
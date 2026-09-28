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
    public DbSet<Cinema> Cinemas{get;set;}
    public DbSet<Movie> Movies{get;set;}
    public DbSet<Show> Shows{get;set;}
    public DbSet<Booking> Bookings{get;set;}


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Movie>()
                    .HasOne(m=>m.Cinema)
                    .WithMany(c=>c.Movies)
                    .HasForeignKey(m=>m.CinemaId)
                    .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Show>()
                    .HasOne(s=>s.Cinema)
                    .WithMany(c=>c.Shows)
                    .HasForeignKey(s=>s.CinemaId)
                    .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Show>()
                    .HasOne(s=>s.Movie)
                    .WithMany(m=>m.Shows)
                    .HasForeignKey(s=>s.MovieId)
                    .OnDelete(DeleteBehavior.Cascade);
    }
}
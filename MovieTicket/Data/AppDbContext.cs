using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MovieTicket.enums;
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
    public DbSet<Payment> Payments{get;set;}


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

        modelBuilder.Entity<Payment>()
                    .HasOne(p=>p.Booking)
                    .WithMany(b=>b.Payments)
                    .HasForeignKey(p=>p.BookingId);

        modelBuilder.Entity<Booking>()
                    .Property(b=>b.PricePP)
                    .HasPrecision(18,2);

        modelBuilder.Entity<Booking>()
                    .Property(b=>b.TotalAmount)
                    .HasPrecision(18,2);
        
        modelBuilder.Entity<Payment>()
                    .Property(p=>p.Amount)
                    .HasPrecision(18,2);

        modelBuilder.Entity<Booking>()
                    .Property(b=>b.Status)
                    .HasDefaultValue(BookingStatus.Pending);

        modelBuilder.Entity<Payment>()
                    .Property(p=>p.Status)
                    .HasDefaultValue(PaymentStatus.Pending);

        modelBuilder.Entity<Payment>()
                    .Property(p=>p.Provider)
                    .IsRequired()
                    .HasMaxLength(50);

        modelBuilder.Entity<Payment>()
                    .Property(p=>p.TransactionId)
                    .HasMaxLength(100);

    }
}
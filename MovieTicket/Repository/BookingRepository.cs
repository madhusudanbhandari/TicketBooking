using Microsoft.EntityFrameworkCore;
using MovieTicket.Data;
using MovieTicket.Interface;
using MovieTicket.Model;

namespace MovieTicket.Repository;

public class BookingRepository : IBookingRepository
{
    private readonly AppDbContext _context;
    public BookingRepository(AppDbContext context)
    {
        _context=context;
    }

    public async Task<Show?> GetShowAsync(int showId)
    {
        return await _context.Shows.FirstOrDefaultAsync(s=>s.Id==showId);
    }
    public async Task<Booking?> GetBookingAsync(int id)
    {
        return await _context.Bookings.FirstOrDefaultAsync(b=>b.Id==id);
    }

    public async Task<List<Booking>> GetAllBookingsAsync()
    {
        return await _context.Bookings
                            .OrderBy(b=>b.Id)
                            .ToListAsync();
    }

    public async Task AddBookingAsync(Booking booking)
    {
        await _context.Bookings.AddAsync(booking);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
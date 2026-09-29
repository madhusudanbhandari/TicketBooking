using MovieTicket.Model;

namespace MovieTicket.Interface;

public interface IBookingRepository
{
    public Task<Show?> GetShowAsync(int showId);
    public Task<Booking?> GetBookingAsync(int id);
    public Task<List<Booking>> GetAllBookingsAsync();
    public Task AddBookingAsync(Booking booking);
    public Task SaveChangesAsync();
}
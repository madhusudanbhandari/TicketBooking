using MovieTicket.Model;

namespace MovieTicket.Interface;

public interface IBookingRepository
{
    public Task<Booking?> GetBookingAsync(int id);
    public Task<List<Booking>> GetAllBookingsAsync();
    public Task AddBookingAsync(Booking booking);
    public Task SaveChangesAsync();
}
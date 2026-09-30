using MovieTicket.Model;

namespace MovieTicket.Repository;

public interface IPaymentRepository
{
    Task<Payment?> GetPendingPaymentByBookingIdAsync(int bookingId);
    Task<Payment?> GetPaymentByBookingIdAsync(int id);
    Task SaveChangesAsync();
}
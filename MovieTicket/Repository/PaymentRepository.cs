using Microsoft.EntityFrameworkCore;
using MovieTicket.Data;
using MovieTicket.Model;

namespace MovieTicket.Repository;


public class PaymentRepository : IPaymentRepository
{
    private readonly AppDbContext _context;
    public PaymentRepository(AppDbContext context)
    {
        _context=context;
    }


    public async Task<Payment?> GetPendingPaymentByBookingIdAsync(int bookingId)
    {
        return await _context.Payments
                    .Include(p=>p.Booking)
                    .FirstOrDefaultAsync(p=>p.BookingId==bookingId &&
                        p.Status==enums.PaymentStatus.Pending);
                    
    }

    public async Task<Payment?> GetPaymentByBookingIdAsync(int id)
    {
        return await _context.Payments
                    .Include(p=>p.Booking)
                    .FirstOrDefaultAsync(p=>p.BookingId==id);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
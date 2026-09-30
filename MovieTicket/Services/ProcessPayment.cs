using MovieTicket.Data;
using MovieTicket.Dtos;
using MovieTicket.Exceptions;
using MovieTicket.Interface;
using MovieTicket.Repository;

namespace MovieTicket.Services;


public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly AppDbContext _context;
    public PaymentService(IPaymentRepository paymentRepository, AppDbContext context)
    {
        _paymentRepository=paymentRepository;
        _context=context;
    }

    public async Task ProcessPaymentAsync(int userId, ProcessPaymentDto dto)
    {
        var payment=await _paymentRepository.GetPendingPaymentByBookingIdAsync(dto.BookingId);

        if (payment == null)
        {
            throw new NotFoundException("Cannot find the booking");
        }
        if (payment.Booking.UserId != userId)
        {
            throw new BadRequestException("You cannot pay for this booking");
        }

        await using var transaction=await _context.Database.BeginTransactionAsync();

        try
        {
            if (dto.IsSucessfull)
            {
                payment.Status=enums.PaymentStatus.Successfull;
                payment.PaidAt=DateTime.UtcNow;
                payment.Booking.Status=enums.BookingStatus.Confirmed;
            }
            else
            {
                payment.Status=enums.PaymentStatus.Failed;
            }
            await _paymentRepository.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}
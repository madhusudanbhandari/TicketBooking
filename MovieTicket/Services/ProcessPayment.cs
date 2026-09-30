using MovieTicket.Data;
using MovieTicket.Dtos;
using MovieTicket.Exceptions;
using MovieTicket.Interface;
using MovieTicket.Repository;

namespace MovieTicket.Services;


public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IPaymentProvider _paymentProvider;
    private readonly AppDbContext _context;
    public PaymentService(IPaymentRepository paymentRepository, AppDbContext context, IPaymentProvider paymentProvider)
    {
        _paymentRepository=paymentRepository;
        _context=context;
        _paymentProvider=paymentProvider;
    }

    public async Task ProcessPaymentAsync(int userId, ProcessPaymentDto dto)
    {
        //var payment=await _paymentRepository.GetPendingPaymentByBookingIdAsync(dto.BookingId);


        var payment=await _paymentRepository.GetPaymentByBookingIdAsync(dto.BookingId);  //for checking duplicate processing/ retry mechanism

        if (payment == null)
        {
            throw new NotFoundException("Cannot find the booking");
        }
        if (payment.Booking.UserId != userId)
        {
            throw new BadRequestException("You cannot pay for this booking");
        }
        if (payment.Status != enums.PaymentStatus.Pending)
        {
            throw new BadRequestException("The payment has been already processed");
        }

        var result=await _paymentProvider.ProcessPaymentAsync(payment.Amount);

        await using var transaction=await _context.Database.BeginTransactionAsync();

        try
        {
            if (result.IsSuccessfull)
            {
                payment.Status=enums.PaymentStatus.Successfull;
                payment.PaidAt=DateTime.UtcNow;
                payment.Booking.Status=enums.BookingStatus.Confirmed;
                payment.TransactionId=result.TransactionId;
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
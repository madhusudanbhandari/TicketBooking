using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MovieTicket.Data;
using MovieTicket.Dtos;
using MovieTicket.Exceptions;
using MovieTicket.Interface;
using MovieTicket.Model;

namespace MovieTicket.Services;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IMapper _mapper;
    private readonly AppDbContext _context;

    public BookingService(IBookingRepository bookingRepository,IMapper mapper,AppDbContext context)
    {
        _bookingRepository=bookingRepository;
        _mapper=mapper;
        _context=context;
    }

    public async Task<ViewBookingDto> CreateBookingAsync(int userId,CreateBookingDto dto)
    {
        var show=await _bookingRepository.GetShowAsync(dto.ShowId);
        if (show == null)
        {
            throw new NotFoundException("Cannot find that show");
        }
        if (dto.TicketQuantity <= 0)
        {
            throw new BadRequestException("Ticket cannot be less than 1");
        }

        using var transaction=await _context.Database.BeginTransactionAsync();
        try
        {
            
        var booking=new Booking
        {
            TicketQuantity=dto.TicketQuantity,
            PricePP=show.Price,
            TotalAmount=show.Price*dto.TicketQuantity,
            UserId=userId,
            ShowId=dto.ShowId
        };

        await _bookingRepository.AddBookingAsync(booking);

        var payment=new Payment
        {
            Booking=booking,
            Amount=booking.TotalAmount,
            Provider="Mock",
            Status=enums.PaymentStatus.Pending,
            CreatedAt=DateTime.UtcNow,
        };

        await _context.Payments.AddAsync(payment);
        await _bookingRepository.SaveChangesAsync();

        await transaction.CommitAsync();
    
        return _mapper.Map<ViewBookingDto>(booking);

        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }

    }

    public async Task<ViewBookingDto?> ViewBookingByIdAsync(int id)
    {
        var booking=await _bookingRepository.GetBookingAsync(id);
        if (booking == null)
        {
            throw new NotFoundException("Cannot find the booking");
        }
        
        return _mapper.Map<ViewBookingDto>(booking);
    }

    public async Task<List<ViewBookingDto>> ViewAllBookingsAsync()
    {
        var bookings=await _bookingRepository.GetAllBookingsAsync();

        return _mapper.Map<List<ViewBookingDto>>(bookings);
    }
}
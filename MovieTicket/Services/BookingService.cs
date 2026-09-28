using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using MovieTicket.Dtos;
using MovieTicket.Exceptions;
using MovieTicket.Interface;
using MovieTicket.Model;

namespace MovieTicket.Services;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IMapper _mapper;

    public BookingService(IBookingRepository bookingRepository,IMapper mapper)
    {
        _bookingRepository=bookingRepository;
        _mapper=mapper;
    }

    public async Task<ViewBookingDto> CreateBookingAsync(int userId,CreateBookingDto dto)
    {
        var booking=new Booking
        {
            TicketQuantity=dto.TicketQuantity,
            PricePP=dto.PricePP,
            TotalAmount=dto.TotalAmount,
            UserId=userId,
            CinemaId=dto.CinemaId,
            MovieId=dto.MovieId,
            ShowId=dto.ShowId
        };

        await _bookingRepository.AddBookingAsync(booking);
        await _bookingRepository.SaveChangesAsync();

        return _mapper.Map<ViewBookingDto>(booking);
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
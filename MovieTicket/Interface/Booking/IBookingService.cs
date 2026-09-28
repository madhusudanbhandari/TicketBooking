using MovieTicket.Dtos;

namespace MovieTicket.Interface;

public interface IBookingService
{
    public Task<ViewBookingDto> CreateBookingAsync(int userId,CreateBookingDto dto);
    public Task<ViewBookingDto?> ViewBookingByIdAsync(int id);
    public Task<List<ViewBookingDto>> ViewAllBookingsAsync();

}
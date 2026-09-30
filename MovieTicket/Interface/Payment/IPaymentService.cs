using MovieTicket.Dtos;

namespace MovieTicket.Interface;

public interface IPaymentService
{
    public Task ProcessPaymentAsync(int userId, ProcessPaymentDto dto);
}
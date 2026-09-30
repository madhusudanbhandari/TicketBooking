using MovieTicket.Dtos;

namespace MovieTicket.Interface;

public interface IPaymentProvider
{
    public Task<PaymentResult> ProcessPaymentAsync(decimal amount); 
}
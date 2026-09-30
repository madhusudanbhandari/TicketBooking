using MovieTicket.Dtos;
using MovieTicket.Interface;

namespace MovieTicket.Services;


public class MockPaymentProvider : IPaymentProvider
{
    public async Task<PaymentResult> ProcessPaymentAsync(decimal amount)
    {
        return new PaymentResult
        {
            IsSuccessfull=true,
            TransactionId=Guid.NewGuid().ToString(),
        };
    }
}
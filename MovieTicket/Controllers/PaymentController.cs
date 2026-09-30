using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using MovieTicket.Dtos;
using MovieTicket.Interface;

namespace MovieTicket.controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentController : ControllerBase
{
    private readonly IPaymentService _paymentService;

    public PaymentController(IPaymentService paymentService)
    {
        _paymentService=paymentService;
    }

    [HttpPost("process-payment")]
    public async Task<IActionResult> ProcessPaymentAsync(ProcessPaymentDto dto)
    {
        var user=User.FindFirstValue(ClaimTypes.NameIdentifier);    

        if(!int.TryParse(user,out int userId))
        {
            return Unauthorized();
        }

        await _paymentService.ProcessPaymentAsync(userId,dto);

        return Ok(new {message="Payment Processed"});
    }

}
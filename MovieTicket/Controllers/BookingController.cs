using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieTicket.Dtos;
using MovieTicket.Interface;
using MovieTicket.Services;

namespace MovieTicket.controllers;

[ApiController]
[Route("api/[controller]")]
public class BookingController : ControllerBase
{
    private readonly IBookingService _bookingService;
    public BookingController(IBookingService bookingService)
    {
        _bookingService=bookingService;
    }

    [HttpPost("create-booking")]
    public async Task<IActionResult> CreateBooking(CreateBookingDto dto)
    {
        var claim=User.FindFirstValue(ClaimTypes.NameIdentifier);
        if(!int.TryParse(claim,out int userId))
        {
            return Unauthorized();
        }

        var booking=await _bookingService.CreateBookingAsync(userId,dto);
        return Ok(booking);
    }

    [HttpGet("view-booking")]
    public async Task<IActionResult> ViewBookingAsync(int id)
    {
        var booking=await _bookingService.ViewBookingByIdAsync(id);

        return Ok(booking);
    }

    [HttpGet("view-all-bookings")]
    public async Task<IActionResult> ViewAllBookingsAsync()
    {
        var bookings=await _bookingService.ViewAllBookingsAsync();
        return Ok(bookings);
    }
}
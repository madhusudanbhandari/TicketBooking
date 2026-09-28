using System.Formats.Asn1;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieTicket.Dtos;
using MovieTicket.Interface;

namespace MovieTicket.controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CinemaController : ControllerBase
{
    private readonly ICinemaService _cinemaService;

    public CinemaController(ICinemaService cinemaService)
    {
        _cinemaService=cinemaService;
    }
    
    [Authorize(Roles ="Manager")]
    [HttpPost("register-cinema")]
    public async Task<IActionResult> RegisterCinemaAsync(RegisterCinemaDto dto)
    {
        var claim=User.FindFirstValue(ClaimTypes.NameIdentifier);

        if(!int.TryParse(claim,out int userId)){
            return Unauthorized();
        }

        var cinema=await _cinemaService.RegisterCinema(userId,dto);

        return Ok(cinema);
    }

    [HttpGet("view-cinema")]
    public async Task<IActionResult> ViewCinemaAsync(int id)
    {
        var cinema=await _cinemaService.GetCinemaAsync(id);
        return Ok(cinema);
    }

    [HttpGet("view-all-cinemas")]
    public async Task<IActionResult> ViewAllCinemas()
    {
        var cinemas=await _cinemaService.ViewAllCinemas();
        return Ok(cinemas);
    }

    [Authorize(Roles ="Manager")]
    [HttpPatch("update-cinema")]
    public async Task<IActionResult> UpdateCinemaAsync(int id, UpdateCinemaDto dto)
    {

        var claim=User.FindFirstValue(ClaimTypes.NameIdentifier);

        if(!int.TryParse(claim,out int userId))
        {
            return Unauthorized();
        }

        var cinema=await _cinemaService.UpdateCinemaAsync(id,userId,dto);
        return Ok(cinema);
    }


    [Authorize(Roles ="Manager,Admin")]
    [HttpDelete("delete-cinema")]
    public async Task<IActionResult> DeleteCinemaAsync(int id)
    {
        var claim=User.FindFirstValue(ClaimTypes.NameIdentifier);

        if(!int.TryParse(claim,out int userId))
        {
            return Unauthorized();
        }
        var cinema=await _cinemaService.DeleteCinemaAsync(id,userId);

        return Ok(cinema);
    }

   

    
}
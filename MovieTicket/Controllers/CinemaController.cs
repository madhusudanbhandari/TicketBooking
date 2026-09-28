using System.Formats.Asn1;
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
        var cinema=await _cinemaService.RegisterCinema(dto);

        return Ok(cinema);
    }

    [HttpGet("view-cinema")]
    public async Task<IActionResult> ViewCinemaAsync(int id)
    {
        var cinema=await _cinemaService.GetCinemaAsync(id);
        return Ok(cinema);
    }

    [Authorize(Roles ="Manager")]
    [HttpPatch("update-cinema")]
    public async Task<IActionResult> UpdateCinemaAsync(int id, UpdateCinemaDto dto)
    {
        var cinema=await _cinemaService.UpdateCinemaAsync(id,dto);
        return Ok(cinema);
    }


    [Authorize(Roles ="Manager,Admin")]
    [HttpDelete("delete-cinema")]
    public async Task<IActionResult> DeleteCinemaAsync(int id)
    {
        var cinema=await _cinemaService.DeleteCinemaAsync(id);

        return Ok(cinema);
    }

   

    
}
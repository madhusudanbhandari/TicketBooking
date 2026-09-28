using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieTicket.Dtos;
using MovieTicket.Interface;
using MovieTicket.Model;

namespace MovieTicket.controllers;


[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ShowController : ControllerBase
{
    private readonly IShowService _showService;
    public ShowController(IShowService showService)
    {
        _showService=showService;
    }

    [HttpPost("add-show")]
    [Authorize(Roles ="Manager")]
    public async Task<IActionResult> AddShowAsync(AddShowDto dto)
    {
        var show=await _showService.AddShowAsync(dto);
        return Ok(show);
    }

    [HttpGet("see-show/{id}")]
    public async Task<IActionResult> SeeShowAsync(int id)
    {
        var show=await _showService.ViewShowByIdAsync(id);
        return Ok(show);
    }

    [HttpGet("see-all-shows")]
    public async Task<IActionResult> SeeAllShowsAsync()
    {
        var shows=await _showService.ViewAllShowsAsync();
        return Ok(shows);
    }

    [Authorize(Roles ="Manager")]
    [HttpPatch("update-show")]
    public async Task<IActionResult> UpdateShowsAsynC(int id, UpdateShowDto dto)
    {
        var show=await _showService.UpdateShowAsync(id,dto);
        return Ok(show);
    }

    [Authorize(Roles ="Manager, Admin")]
    [HttpDelete("delete-shows")]
    public async Task<IActionResult> DeleteShowsAsync(int id)
    {
        var show=await _showService.DeleteShowAsync(id);
        return Ok(show);
    }
}
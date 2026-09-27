using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieTicket.Dtos;
using MovieTicket.Interface;
using MovieTicket.Model;

namespace MovieTicket.controllers;


[ApiController]
[Route("api/[controller]")]
[Authorize]
public class MovieController : ControllerBase
{
    private readonly IMovieService _movieService;

    public MovieController(IMovieService movieService)
    {
        _movieService=movieService;
    }

    [Authorize(Roles ="Manager")]
    [HttpPost("add-movie")]
    public async Task<IActionResult> AddMovieAsync(AddMovieDto dto)
    {
        var movie=await _movieService.AddMovieAsync(dto);

        return Ok(movie);
    }

    [HttpGet("view-movie/{id}")]
    public async Task<IActionResult> ViewMovieAsync(int id)
    {
        var movie=await _movieService.ViewMovieAsync(id);
        return Ok(movie);
    }

    [HttpGet("view-all-movies")]
    public async Task<IActionResult> ViewAllMoviesAsync()
    {
        var movies=await _movieService.ViewAllMovies();
        return Ok(movies);
    }

    [Authorize(Roles ="Manager")]
    [HttpPatch("update-movie")]
    public async Task<IActionResult> UpdateMovieAsync(int id, UpdateMovieDto dto)
    {
        var movie=await _movieService.UpdateMovieAsync(id,dto);

        return Ok(movie);
    }

    [Authorize(Roles ="Manager,Admin")]
    [HttpDelete("delete-movie")]
    public async Task<IActionResult> DeleteMovieAsync(int id)
    {
        var movie=await _movieService.DeleteMovieAsync(id);

        return Ok(movie);
    }
}
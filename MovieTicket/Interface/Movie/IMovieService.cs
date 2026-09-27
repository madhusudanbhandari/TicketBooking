using MovieTicket.Dtos;
using MovieTicket.Model;

namespace MovieTicket.Interface;

public interface IMovieService
{
    public Task<ViewMovieDto> AddMovieAsync(AddMovieDto dto);
    public Task<List<ViewMovieDto>> ViewAllMovies();
    public Task<ViewMovieDto?> ViewMovieAsync(int id);
    public Task<ViewMovieDto?> UpdateMovieAsync(int id, UpdateMovieDto dto);
    public Task<string?> DeleteMovieAsync(int id);
}
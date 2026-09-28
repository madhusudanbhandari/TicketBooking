using MovieTicket.Dtos;
using MovieTicket.Model;

namespace MovieTicket.Interface;

public interface ICinemaService
{
    public Task<ViewCinemaDto> RegisterCinema(int userId,RegisterCinemaDto dto);
    public Task<ViewCinemaDto?> GetCinemaAsync(int id);
    public Task<List<ViewCinemaDto>> ViewAllCinemas();
    public Task<ViewCinemaDto?>UpdateCinemaAsync(int id,int userId,UpdateCinemaDto dto); 
    public Task<string?> DeleteCinemaAsync(int id,int userId);
}
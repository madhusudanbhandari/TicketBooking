using MovieTicket.Dtos;
using MovieTicket.Model;

namespace MovieTicket.Interface;

public interface ICinemaService
{
    public Task<ViewCinemaDto> RegisterCinema(RegisterCinemaDto dto);
    public Task<ViewCinemaDto?> GetCinemaAsync(int id);
    public Task<ViewCinemaDto?>UpdateCinemaAsync(int id,UpdateCinemaDto dto); 
    public Task<string?> DeleteCinemaAsync(int id);
}
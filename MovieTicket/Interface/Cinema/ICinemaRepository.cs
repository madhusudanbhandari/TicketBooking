
using MovieTicket.Model;

namespace MovieTicket.Interface;

public interface ICinemaRepository
{
    public Task<Cinema?> GetCinemaAsync(int id);
    public Task<List<Cinema>> GetAllCinemas();
    public Task AddCinemaAsync(Cinema cinema);
    public void RemoveCinema(Cinema cinema);
    public Task SaveChangesAsync();
}
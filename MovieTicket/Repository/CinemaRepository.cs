using Microsoft.EntityFrameworkCore;
using MovieTicket.Data;
using MovieTicket.Interface;
using MovieTicket.Model;

namespace MovieTicket.Repository;

public class CinemaRepository : ICinemaRepository
{
    private readonly AppDbContext _context;

    public CinemaRepository(AppDbContext context)
    {
        _context=context;
    }

    public async Task<Cinema?> GetCinemaAsync(int id)
    {
        return await _context.Cinemas.FirstOrDefaultAsync(c=>c.Id==id);
    }

    public async Task<List<Cinema>> GetAllCinemas()
    {
        return await _context.Cinemas
                            .OrderBy(c=>c.Id)
                            .ToListAsync();
    }
    public async Task AddCinemaAsync(Cinema cinema)
    {
        await _context.Cinemas.AddAsync(cinema);
    }

    public void RemoveCinema(Cinema cinema)
    {
         _context.Cinemas.Remove(cinema);
    }    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
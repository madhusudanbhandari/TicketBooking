using Microsoft.EntityFrameworkCore;
using MovieTicket.Data;
using MovieTicket.Interface;
using MovieTicket.Model;

namespace MovieTicket.Repository;

public class ShowRepository : IShowRepository
{
    private readonly AppDbContext _context;
    public ShowRepository(AppDbContext context)
    {
        _context=context;
    }

    public async Task<Show?> GetShowAsync(int id)
    {
        return await _context.Shows.FirstOrDefaultAsync(s=>s.Id==id);
    }

    public async Task<List<Show>> GetAllShowsAsync()
    {
        return await _context.Shows
                    .OrderBy(s=>s.Id)
                    .ToListAsync();
    }

    public async Task AddShowAsync(Show show)
    {
        await _context.Shows.AddAsync(show);
    }

    public void RemoveShowAsync(Show show)
    {
         _context.Shows.Remove(show);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
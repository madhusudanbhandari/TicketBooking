using System.Runtime;
using Microsoft.EntityFrameworkCore;
using MovieTicket.Data;
using MovieTicket.Interface;
using MovieTicket.Model;

namespace MovieTicket.Repository;

public class MovieRepository : IMovieRepository
{
    private readonly AppDbContext _context;

    public MovieRepository(AppDbContext context)
    {
        _context=context;
    }

    public async Task<List<Movie>> GetAllMoviesAsync()
    {
        return await _context.Movies.ToListAsync();
    }
    public async Task<Movie?> GetMovieAsync(int id)
    {
        return await _context.Movies.FirstOrDefaultAsync(m=>m.Id==id);
    }

    public async Task AddMovieAsync(Movie movie)
    {
        await _context.Movies.AddAsync(movie);
    }

    public  void RemoveMovieAsync(Movie movie)
    {
         _context.Movies.Remove(movie);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
using MovieTicket.Model;

namespace MovieTicket.Interface;

public interface IMovieRepository
{
    public Task<List<Movie>> GetAllMoviesAsync();
    public Task<Movie?> GetMovieAsync(int id);
    public Task AddMovieAsync(Movie movie);
    public void RemoveMovieAsync(Movie movie);
    public Task SaveChangesAsync();
}
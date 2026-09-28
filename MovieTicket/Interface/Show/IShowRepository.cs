using MovieTicket.Model;

namespace MovieTicket.Interface;

public interface IShowRepository
{
    public Task<Show?> GetShowAsync(int id);
    public Task<List<Show>> GetAllShowsAsync();
    public Task AddShowAsync(Show show);
    public void RemoveShowAsync(Show show);
    public Task SaveChangesAsync();
}
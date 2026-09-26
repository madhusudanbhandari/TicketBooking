using MovieTicket.Model;

namespace MovieTicket.Interface.Auth;

public interface IAuthRepository
{
    public Task<User?> GetUserAsync(string emil);

    public Task AddUserAsync(User user);
    public Task SaveChangesAsync();
}
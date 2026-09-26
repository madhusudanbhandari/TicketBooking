using Microsoft.EntityFrameworkCore;
using MovieTicket.Data;
using MovieTicket.Interface.Auth;
using MovieTicket.Model;

namespace MovieTicket.Repository;

public class AuthRepository : IAuthRepository
{
    private readonly AppDbContext _context;
    public AuthRepository(AppDbContext context)
    {
        _context=context;
    }
    
    public async Task<User?> GetUserAsync(string email)
    {
        var user=await _context.Users.FirstOrDefaultAsync(u=>u.Email==email);

        return user;
    }

    public async Task AddUserAsync(User user)
    {
        await _context.Users.AddAsync(user);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
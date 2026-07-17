using BotChat.App.UserLogic;
using BotChat.Domain;
using Microsoft.EntityFrameworkCore;

namespace BotChat.Infrastructure.Persistant.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _db;

    public UserRepository(AppDbContext db)
    {
        _db = db;
    }

    public Task<User?> GetUserAsync()
    {
        return _db.Users.FirstOrDefaultAsync();
    }

    public async Task<User> CreateUserAsync(User user)
    {
        var newuser = _db.Users.Add(user);
        await _db.SaveChangesAsync();
        return newuser.Entity;
    }
    
    public async Task<User> UpdateUserAsync(User user)
    {
        await _db.SaveChangesAsync();
        return user;
    }
}
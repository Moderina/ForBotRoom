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

    public async Task<User?> GetUserAsync()
    {
        return await _db.Users.FirstOrDefaultAsync();
    }
}
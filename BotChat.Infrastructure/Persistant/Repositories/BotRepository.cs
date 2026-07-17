using BotChat.App.BotLogic;
using BotChat.Domain.Bots;
using Microsoft.EntityFrameworkCore;

namespace BotChat.Infrastructure.Persistant.Repositories;

public class BotRepository : IBotRepository
{
    private readonly AppDbContext _db;

    public BotRepository(AppDbContext db)
    {
        _db = db;
    }
    public Task<List<Bot>> GetBotsAsync()
    {
        return _db.Bots.Include(b => b.User).ToListAsync();
    }

    public Task<Bot?> GetBotByIdAsync(Guid botId)
    {
        return _db.Bots.Include(b => b.User).FirstOrDefaultAsync(b =>b.UserId == botId);
    }

    public async Task<Bot> AddBotAsync(Bot bot)
    {
        var newbot = _db.Bots.Add(bot);
        await _db.SaveChangesAsync();
        return newbot.Entity;
    }

    public async Task<Bot> UpdateBotAsync(Bot bot)
    {
        await _db.SaveChangesAsync();
        return bot;
    }

    public async Task DeleteBotAsync(Bot bot)
    {
        _db.Bots.Remove(bot);
        await _db.SaveChangesAsync();
    }
}
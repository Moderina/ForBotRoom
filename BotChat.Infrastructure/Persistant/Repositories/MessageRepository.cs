using BotChat.App.MessageLogic;
using BotChat.Domain.Chats;
using Microsoft.EntityFrameworkCore;

namespace BotChat.Infrastructure.Persistant.Repositories;

public class MessageRepository : IMessageRepository
{
    private readonly AppDbContext _db;

    public MessageRepository(AppDbContext db)
    {
        _db = db;
    }
    
    public async Task<Message> AddAsync(Message message)
    {
        _db.Messages.Add(message);
        await _db.SaveChangesAsync();
        return message;
    }

    public Task<List<Message>> GetChatHistoryAsync(Guid chatId, int limit, DateTime lastMessageTime)
    {
        return _db.Messages
            .Include(m => m.Author)
            .Where(m => m.ChatId == chatId && m.Timestamp <= lastMessageTime)
            .OrderByDescending(m => m.Timestamp)
            .Take(limit)
            .OrderBy(m => m.Timestamp)
            .ToListAsync();
    }
    
    public Task<List<Message>> GetChatHistoryNewerThanAsync(Guid chatId, DateTime lastMessageTime, int limit)
    {
        return _db.Messages
            .Include(m => m.Author)
            .Where(m => m.ChatId == chatId && m.Timestamp > lastMessageTime)
            .OrderByDescending(m => m.Timestamp)
            .Take(limit)
            .OrderBy(m => m.Timestamp)
            .ToListAsync();
    }

    public Task<List<Message>> GetByChatAsync(Guid chatId)
    {
        return _db.Messages
            .Where(m => m.ChatId == chatId)
            .OrderBy(m => m.Timestamp)
            .ToListAsync();
    }
}
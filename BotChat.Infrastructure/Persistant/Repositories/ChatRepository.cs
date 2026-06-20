using BotChat.App.ChatLogic;
using BotChat.Domain.Chats;
using Microsoft.EntityFrameworkCore;

namespace BotChat.Infrastructure.Persistant.Repositories;

public class ChatRepository : IChatRepository
{
    private readonly AppDbContext _db;
    
    public ChatRepository(AppDbContext db)
    {
        _db = db;
    }
    public Task<List<Chat>> GetChatsAsync()
    {
        return _db.Chats.ToListAsync();
    }

    public Task<Chat?> GetChatAsync(long chatId)
    {
        return _db.Chats.FindAsync(chatId).AsTask();
    }

    public Task AddChatAsync(Chat chat)
    {
        _db.Chats.Add(chat);
        return _db.SaveChangesAsync();
    }

    public Task DeleteChatAsync(long chatId)
    {
        var chat = _db.Chats.Find(chatId);
        _db.Chats.Remove(chat);
        return _db.SaveChangesAsync();
    }
}
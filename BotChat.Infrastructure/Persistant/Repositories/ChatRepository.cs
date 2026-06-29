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
    public Task<List<Chat>> GetActiveChatsAsync()
    {
        return _db.Chats.Where(chat => !chat.Disabled).ToListAsync();
    }

    public Task<Chat?> GetChatAsync(long chatId)
    {
        return _db.Chats.FindAsync(chatId).AsTask();
    }

    public async Task AddChatAsync(Chat chat)
    {
        _db.Chats.Add(chat);
        await _db.SaveChangesAsync();
    }

    public async Task<bool> DisableChatAsync(Guid chatId)
    {
        var chat = await _db.Chats.FindAsync(chatId);
        if (chat == null) return false;
        chat.Disabled = true;
        await _db.SaveChangesAsync();
        return true;
    }

    //TODO: move finding chat to service
    public Task DeleteChatAsync(long chatId)
    {
        var chat = _db.Chats.Find(chatId);
        _db.Chats.Remove(chat);
        return _db.SaveChangesAsync();
    }
}
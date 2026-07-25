using BotChat.App.ConversationLogic;
using BotChat.Domain.Chats;

namespace BotChat.Infrastructure.Persistant.Repositories;

public class ChatMemoryRepository : IChatMemoryRepository
{
    private readonly AppDbContext _db;
    
    public ChatMemoryRepository(AppDbContext db)
    {
        _db = db;
    }
    
    public Task<ChatMemory?> GetChatMemoryByIdAsync(Guid chatId)
    {
        return _db.ChatMemories.FindAsync(chatId).AsTask();
    }

    public async Task UpsertAsync(ChatMemory memory)
    {
        var existingMemory = await GetChatMemoryByIdAsync(memory.ChatId);
        if (existingMemory == null)
        {
            _db.ChatMemories.Add(memory);
        }
        else
        {
            existingMemory = memory;
            _db.ChatMemories.Update(existingMemory);
        }
        await _db.SaveChangesAsync();
    }
}
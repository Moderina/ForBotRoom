using BotChat.Domain.Chats;

namespace BotChat.App.ConversationLogic;

public interface IChatMemoryRepository
{
    public Task<ChatMemory?> GetChatMemoryByIdAsync(Guid chatId);
    public Task UpsertAsync(ChatMemory memory);
}
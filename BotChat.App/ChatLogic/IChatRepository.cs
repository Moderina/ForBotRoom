using BotChat.Domain.Chats;

namespace BotChat.App.ChatLogic;

public interface IChatRepository
{
    Task<List<Chat>> GetActiveChatsAsync();
    
    Task<Chat?> GetChatAsync(Guid chatId);
    
    Task AddChatAsync(Chat chat);
    
    Task<bool> DisableChatAsync(Guid chatId);
    
    Task DeleteChatAsync(long chatId);
}
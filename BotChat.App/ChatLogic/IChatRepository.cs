using BotChat.Domain.Chats;

namespace BotChat.App.ChatLogic;

public interface IChatRepository
{
    Task<List<Chat>> GetActiveChatsAsync();
    
    Task<Chat?> GetChatAsync(long chatId);
    
    Task AddChatAsync(Chat chat);
    
    Task<bool> DisableChatAsync(Guid chatId);
    
    Task DeleteChatAsync(long chatId);
}
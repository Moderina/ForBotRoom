using BotChat.Domain.Chats;

namespace BotChat.App.ChatLogic;

public interface IChatRepository
{
    Task<List<Chat>> GetChatsAsync();
    
    Task<Chat?> GetChatAsync(long chatId);
    
    Task AddChatAsync(Chat chat);
    
    Task DeleteChatAsync(long chatId);
}
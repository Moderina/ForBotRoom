using BotChat.Contracts.Chat;

namespace BotChat.App.ChatLogic;

public interface IChatService
{
    Task<List<ChatDto>> GetActiveChatsAsync();

    Task<ChatDto> CreateChatAsync(Guid userId, CreateChatRequest request);
    
    Task DisableChatAsync(Guid chatId);

    Task DeleteChatAsync(Guid chatId);
}
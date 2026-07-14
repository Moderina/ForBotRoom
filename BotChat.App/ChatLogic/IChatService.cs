using BotChat.Contracts.Chat;
using BotChat.Domain;

namespace BotChat.App.ChatLogic;

public interface IChatService
{
    Task<List<ChatListItemDto>> GetActiveChatsAsync();
    Task<List<User>> GetHumanMembersOfChatAsync(Guid chatId);
    
    Task<ChatDetailsDto> GetChatDetailsAsync(Guid chatId);
    Task<ChatListItemDto> CreateChatAsync(Guid userId, CreateChatRequest request);
    Task DisableChatAsync(Guid chatId);
    Task DeleteChatAsync(Guid chatId);
}
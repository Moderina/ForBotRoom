using BotChat.Contracts.Chat;
using BotChat.Domain;

namespace BotChat.App.ChatLogic;

public interface IChatService
{
    Task<List<ChatDto>> GetActiveChatsAsync();
    Task<List<User>> GetHumanMembersOfChatAsync(Guid chatId);
    Task<ChatDto> CreateChatAsync(Guid userId, CreateChatRequest request);
    Task DisableChatAsync(Guid chatId);
    Task DeleteChatAsync(Guid chatId);
}
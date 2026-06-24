using BotChat.Contracts.Chat;

namespace BotChat.App.ChatLogic;

public interface IChatService
{
    Task<List<ChatDto>> GetChatsAsync();

    Task<ChatDto> CreateChatAsync(Guid userId, CreateChatRequest request);

    Task DeleteChatAsync(Guid chatId);
}
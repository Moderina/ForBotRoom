using BotChat.Contracts.Chat;

namespace BotChat.App.ChatLogic;

public interface IChatService
{
    List<ChatDto> GetChats();

    ChatDto CreateChat(
        Guid userId,
        CreateChatRequest request);

    Task DeleteChat(
        Guid chatId);
}
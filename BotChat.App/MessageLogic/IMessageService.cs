using BotChat.Contracts.Chat;
using BotChat.Domain.Chats;

namespace BotChat.App.ChatLogic;

public interface IMessageService
{
    public Task<MessageDto> CreateMessageAsync(Guid chatId, Guid authorId, string content);

    public Task<List<Message>> GetChatHistoryAsync(Guid chatId, int length = 20, long lastMessageTime = -1);
}
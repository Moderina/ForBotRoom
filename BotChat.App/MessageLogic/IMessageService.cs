using BotChat.Contracts.Chat;
using BotChat.Domain.Chats;

namespace BotChat.App.ChatLogic;

public interface IMessageService
{
    public Task<MessageDto> CreateMessageAsync(Guid chatId, Guid authorId, string content);

    public Task<List<Message>> GetChatHistoryAsync(Guid chatId, int length = 40, long lastMessageTime = -1);
    public Task<List<Message>> GetChatHistoryNewerThanAsync(Guid chatId, DateTime? lastMessageTime, int length = 40);
}
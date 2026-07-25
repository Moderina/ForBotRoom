using BotChat.Domain.Chats;

namespace BotChat.App.ChatLogic;

public interface IMessageRepository
{
    public Task<Message> AddAsync(Message message);

    public Task<List<Message>> GetChatHistoryAsync(Guid chatId, int limit, DateTime lastMessageTime);
    public Task<List<Message>> GetChatHistoryNewerThanAsync(Guid chatId, DateTime lastMessageTime, int length);
    public Task<List<Message>> GetByChatAsync(Guid chatId);
}
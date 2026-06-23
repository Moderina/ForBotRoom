using BotChat.Domain.Chats;

namespace BotChat.App.ChatLogic;

public interface IMessageService
{
    public void AddMessage(Message message);

    public Task<List<Message>> GetChatHistory(Guid chatId, int length = 20, long lastMessageTime = -1);
}
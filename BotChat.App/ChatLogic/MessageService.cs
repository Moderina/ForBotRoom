using BotChat.Domain.Chats;

namespace BotChat.App.ChatLogic;

public class MessageService : IMessageService
{
    private readonly IMessageRepository _messageRepository;

    public MessageService(IMessageRepository messageRepository)
    {
        _messageRepository = messageRepository;
    }
    
    public void AddMessage(Message message)
    {
        _messageRepository.AddAsync(message);
    }

    public Task<List<Message>> GetChatHistory(Guid chatId, int length = 20, long lastMessageTime = -1)
    {
        var time = lastMessageTime == -1 ? DateTime.Now : new DateTime(lastMessageTime);
        return _messageRepository.GetChatHistoryAsync(chatId, length, time);
    }
}
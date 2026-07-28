using BotChat.Contracts.Chat;
using BotChat.Domain.Chats;

namespace BotChat.App.MessageLogic;

public class MessageService : IMessageService
{
    private readonly IMessageRepository _messageRepository;

    public MessageService(IMessageRepository messageRepository)
    {
        _messageRepository = messageRepository;
    }
    
    public async Task<MessageDto> CreateMessageAsync(Guid chatId, Guid authorId, string content)
    {
        Message message = new Message(chatId, authorId, content);
        await _messageRepository.AddAsync(message);
        var dto = new MessageDto()
        {
            Id = message.Id,
            ChatId = chatId,
            AuthorId = message.AuthorId,
            Content = message.Content,
            Timestamp = message.Timestamp
        };
        return dto;
    }

    public Task<List<Message>> GetChatHistoryAsync(Guid chatId, int length = 40, long lastMessageTime = -1)
    {
        var time = lastMessageTime == -1 ? DateTime.Now : new DateTime(lastMessageTime);
        return _messageRepository.GetChatHistoryAsync(chatId, length, time);
    }
    
    public Task<List<Message>> GetChatHistoryNewerThanAsync(Guid chatId, DateTime? lastMessageTime)
    {
        var time = lastMessageTime ?? DateTime.MinValue;
        return _messageRepository.GetChatHistoryNewerThanAsync(chatId, time);
    }
}
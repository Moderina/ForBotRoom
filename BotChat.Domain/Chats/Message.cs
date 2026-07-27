using BotChat.Domain.Users;

namespace BotChat.Domain.Chats;

public class Message
{
    public Guid Id { get; private set; }
    public Guid ChatId { get; private set; }
    public Guid AuthorId { get; private set; }
    public User Author { get; private set; } = null;
    public string Content { get; private set; }
    public DateTime Timestamp { get; private set; }
    
    public Message() { }
    
    public Message(Guid chatId, Guid authorId, string content)
    {
        Id = Guid.NewGuid();
        ChatId = chatId;
        AuthorId = authorId;
        Content = content;
        Timestamp = DateTime.UtcNow;
    }
}
namespace BotChat.Domain.Chats;

public class Message
{
    public Guid Id { get; private set; }
    public int ChatId { get; private set; }
    public int AuthorId { get; private set; }
    public string Content { get; private set; }
    public DateTime Timestamp { get; private set; }
    
    public Message() { }
    
    public Message(int chatId, int authorId, string content)
    {
        ChatId = chatId;
        AuthorId = authorId;
        Content = content;
        Timestamp = DateTime.UtcNow;
    }
}
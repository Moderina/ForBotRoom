namespace BotChat.Contracts.Chat;

public class MessageDto
{
    public Guid Id { get;  set; }
    public Guid ChatId { get;  set; }
    public Guid AuthorId { get;  set; }

    public string UserType { get; set; } = "user";
    public string Content { get;  set; } = string.Empty;
    public DateTime Timestamp { get;  set; }

    public bool Sent { get; set; } = true;
}
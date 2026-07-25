namespace BotChat.Domain.Chats;

public class ChatMemory
{
    public Guid ChatId { get; set; }          
    public string? Summary { get; set; }            
    public Guid? LastSummarizedMessageId { get; set; }
    public DateTime? LastSummarizedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
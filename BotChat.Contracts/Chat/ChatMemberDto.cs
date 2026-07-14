namespace BotChat.Contracts.Chat;

public class ChatMemberDto
{
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public string Type { get; set; }
}
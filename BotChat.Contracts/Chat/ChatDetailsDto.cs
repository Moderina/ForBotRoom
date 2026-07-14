namespace BotChat.Contracts.Chat;

public class ChatDetailsDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public List<ChatMemberDto> Members { get; init; } = [];
    public List<MessageDto> Messages { get; init; } = [];
}
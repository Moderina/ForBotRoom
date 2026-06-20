namespace BotChat.Contracts.Chat;

public sealed class CreateChatRequest
{
    public Guid UserId { get; set; }
    public string Name { get; init; } = string.Empty;
}
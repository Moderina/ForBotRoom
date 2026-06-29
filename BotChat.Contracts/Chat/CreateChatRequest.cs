namespace BotChat.Contracts.Chat;

public sealed class CreateChatRequest
{
    public string Name { get; init; } = string.Empty;
    public Guid BotId { get; set; }
}
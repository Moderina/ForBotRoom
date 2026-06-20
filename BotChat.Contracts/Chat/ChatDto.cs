namespace BotChat.Contracts.Chat;

public sealed class ChatDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
}
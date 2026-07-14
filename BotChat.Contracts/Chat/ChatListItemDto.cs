namespace BotChat.Contracts.Chat;

public sealed class ChatListItemDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
}
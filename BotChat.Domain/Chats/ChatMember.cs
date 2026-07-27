using BotChat.Domain.Users;

namespace BotChat.Domain.Chats;

public class ChatMember
{
    public Guid ChatId { get; set; }
    public Chat Chat { get; set; } = null!;

    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
}
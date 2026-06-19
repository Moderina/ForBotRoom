using BotChat.Domain.Agents;
using BotChat.Domain.Users;

namespace BotChat.Domain;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public UserType Type { get; set; } = UserType.Bot;
    public Bot? Bot { get; set; } = null!;
    public UserStatus Active {get; set;} = UserStatus.Offline;
    public string? ProfilePictureUrl { get; set; }
    public DateTime CreatedAtUtc { get; set; }

    public User(string name, UserType type)
    {
        Id = Guid.NewGuid();
        Name = name;
        Type = type;
        CreatedAtUtc = DateTime.UtcNow;
    }
}
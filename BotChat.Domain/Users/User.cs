using BotChat.Domain.Bots;

namespace BotChat.Domain.Users;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public UserType Type { get; set; } = UserType.Bot;
    public Bot? Bot { get; set; } = null!;
    public UserStatus Active {get; set;} = UserStatus.Offline;
    public string? ProfilePictureUrl { get; set; }
    public DateTime CreatedAtUtc { get; set; }

    public User(string name, UserType type, string? profilePictureUrl)
    {
        Id = Guid.NewGuid();
        Name = name;
        Type = type;
        ProfilePictureUrl = profilePictureUrl ?? "";
        CreatedAtUtc = DateTime.UtcNow;
    }
}
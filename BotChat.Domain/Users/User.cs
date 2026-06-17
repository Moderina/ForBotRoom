using BotChat.Domain.Users;

namespace BotChat.Domain;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public UserType Type { get; set; } = UserType.Bot;
    public UserStatus Active {get; set;} = UserStatus.Offline;
    public string? ProfilePictureUrl { get; set; }
    public DateTime CreatedAtUtc { get; set; }
}
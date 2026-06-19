namespace BotChat.Contracts.User;

public class UserDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? ProfilePictureUrl { get; init; }
    public int Status { get; init; }
}
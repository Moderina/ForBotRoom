using BotChat.Contracts.User;

namespace BotChat.App.UserLogic;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepo;
    public UserService(IUserRepository  repo)
    {
        _userRepo = repo;
    }
    public async Task<UserDto?> GetUserAsync()
    {
        var user = await _userRepo.GetUserAsync();

        if (user is null)
            return null;

        return new UserDto
        {
            Id = user.Id,
            Name = user.Name,
            Status = (int)user.Active,
            ProfilePictureUrl = $"/api/media/profile-pictures/{user.ProfilePictureUrl}",
        };
    }
}
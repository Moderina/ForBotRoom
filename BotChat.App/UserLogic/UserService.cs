using BotChat.App.UserLogic;
using BotChat.Contracts.User;

namespace BotChat.App.Services;

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
        };
    }
}
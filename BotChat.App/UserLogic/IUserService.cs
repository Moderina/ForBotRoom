using BotChat.Contracts.User;

namespace BotChat.App.UserLogic;

public interface IUserService
{
    Task<UserDto?> GetUserAsync();
}
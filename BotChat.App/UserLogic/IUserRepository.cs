using BotChat.Domain.Users;

namespace BotChat.App.UserLogic;

public interface IUserRepository
{
    Task<User?> GetUserAsync();
    
    Task<User> CreateUserAsync(User user);
    
    Task<User> UpdateUserAsync(User user);
}
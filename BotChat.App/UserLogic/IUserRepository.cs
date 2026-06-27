namespace BotChat.App.UserLogic;

public interface IUserRepository
{
    Task<Domain.User?> GetUserAsync();
    
    Task<Domain.User> CreateUserAsync(Domain.User user);
}
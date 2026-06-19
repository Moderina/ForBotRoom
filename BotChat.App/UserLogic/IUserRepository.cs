namespace BotChat.App.UserLogic;

public interface IUserRepository
{
    Task<Domain.User?> GetUserAsync();
}
using BotChat.Domain.Bots;

namespace BotChat.App.BotLogic;

public interface IBotRepository
{
    Task<List<Bot>> GetBotsAsync();
    Task<Bot?> GetBotByIdAsync(Guid botId);
    Task<Bot> AddBotAsync(Bot bot);
    Task<Bot> UpdateBotAsync(Bot bot);
    Task DeleteBotAsync(Bot bot);
}
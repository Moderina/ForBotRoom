using BotChat.Contracts.Bots;
using BotChat.Domain.Bots;

namespace BotChat.App.BotLogic;

public interface IBotService
{
    Task<List<BotDto>> GetBotsAsync();
    
    Task<BotDetailsDto> GetBotDetailsAsync(Guid id);
    
    Task<BotDetailsDto> CreateBotAsync(CreateBotRequest bot);
    
    Task<BotDetailsDto?> UpdateBotAsync(Guid id, CreateBotRequest bot);
    
    Task DeleteBotAsync(Guid id);
}
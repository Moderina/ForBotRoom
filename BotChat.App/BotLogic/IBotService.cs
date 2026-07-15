using BotChat.Contracts.Bots;
using BotChat.Domain.Bots;

namespace BotChat.App.BotLogic;

public interface IBotService
{
    Task<List<BotDto>> GetBotsAsync();
    
    Task<BotDetailsDto> GetBotDetailsAsync(Guid id);
    
    Task<Bot?> GetBotByIdAsync(Guid id);
    
    Task<BotDetailsDto> CreateBotAsync(string Name, string PersonalityData, string ProfilePicutreUrl);
    
    Task<BotDetailsDto?> UpdateBotAsync(Guid id, string Name, string Personality, string ProfilePicutreUrl);
    
    Task DeleteBotAsync(Guid id);
}
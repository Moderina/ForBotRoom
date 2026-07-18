using BotChat.Contracts.Bots;
using BotChat.Contracts.Storage;
using BotChat.Domain.Bots;

namespace BotChat.App.BotLogic;

public interface IBotService
{
    Task<List<BotDto>> GetBotsAsync();
    
    Task<BotDetailsDto> GetBotDetailsAsync(Guid id);
    
    Task<Bot?> GetBotByIdAsync(Guid id);
    
    Task<BotDetailsDto> CreateBotAsync(string name, PersonalityProfile personalityProfile, FileUpload? fileUpload, CancellationToken cancellationToken);
    
    Task<BotDetailsDto?> UpdateBotAsync(Guid id, string Name, PersonalityProfile? personalityProfile, FileUpload? profilePicture, CancellationToken cancellationToken);
    
    Task DeleteBotAsync(Guid id);
}
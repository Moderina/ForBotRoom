using BotChat.App.UserLogic;
using BotChat.Contracts.Bots;
using BotChat.Domain;
using BotChat.Domain.Bots;
using BotChat.Domain.Users;

namespace BotChat.App.BotLogic;

public class BotService : IBotService
{
    private readonly IUserRepository _userRepository;
    private readonly IBotRepository _botRepository;

    public BotService(IBotRepository botRepository, IUserRepository userRepository)
    {
        _userRepository = userRepository;
        _botRepository = botRepository;
    }
    public async Task<List<BotDto>> GetBotsAsync()
    {
        var bots = await _botRepository.GetBotsAsync();
        var dtos = new List<BotDto>();
        foreach (var b in bots)
        {
            dtos.Add(new BotDto()
            {
                Id = b.UserId,
                Name = b.User.Name
            });
        }
        return dtos;
    }

    public async Task<BotDetailsDto> GetBotDetailsAsync(Guid id)
    {
        var bot = await _botRepository.GetBotByIdAsync(id);

        var personalitydto = new PersonalityDataDto()
        {
            CoreIdentity = bot.PersonalityData.CoreIdentity,
            Dislikes = bot.PersonalityData.Dislikes,
            Likes = bot.PersonalityData.Likes,
            Interests = bot.PersonalityData.Interests,
            Personality = bot.PersonalityData.Personality,
            TextingStyle = bot.PersonalityData.TextingStyle,
            Example = bot.PersonalityData.Example
        };
        return new BotDetailsDto()
        {
            Id = bot.UserId,
            Name = bot.User.Name,
            PersonalityData = personalitydto
        };
    }

    public async Task<BotDetailsDto> CreateBotAsync(CreateBotRequest bot)
    {
        var savedUser = await _userRepository.CreateUserAsync(new User(bot.Name, UserType.Bot));
        var personalityData = new PersonalityData
        {
            CoreIdentity = bot.PersonalityData.CoreIdentity,
            Dislikes = bot.PersonalityData.Dislikes,
            Likes = bot.PersonalityData.Likes,
            Interests = bot.PersonalityData.Interests,
            Personality = bot.PersonalityData.Personality,
            TextingStyle = bot.PersonalityData.TextingStyle,
            Example = bot.PersonalityData.Example
        };
        var newbot =await _botRepository.AddBotAsync(new Bot(savedUser.Id, personalityData));
        return MakeDto(newbot);
    }

    public async Task<BotDetailsDto?> UpdateBotAsync(Guid id, CreateBotRequest bot)
    {
        var savedBot = await _botRepository.GetBotByIdAsync(id);
        if (savedBot == null) return null;
        
        if (!string.IsNullOrWhiteSpace(bot.Name))
        {
            savedBot.User.Name = bot.Name;
        }
        MapPersonalityData(savedBot.PersonalityData, bot.PersonalityData);
        await _botRepository.UpdateBotAsync(savedBot);
        
        return MakeDto(savedBot);
    }

    public Task DeleteBotAsync(Guid id)
    {
        return Task.CompletedTask;
    }
    
    private void MapPersonalityData(PersonalityData data, PersonalityDataDto dto)
    {
        if (!string.IsNullOrWhiteSpace(dto.Personality))
            data.Personality = dto.Personality;

        if (!string.IsNullOrWhiteSpace(dto.Likes))
            data.Likes = dto.Likes;

        if (!string.IsNullOrWhiteSpace(dto.Dislikes))
            data.Dislikes = dto.Dislikes;
        
        if (!string.IsNullOrWhiteSpace(dto.CoreIdentity))
            data.CoreIdentity = dto.CoreIdentity;
        
        if (!string.IsNullOrWhiteSpace(dto.TextingStyle))
            data.TextingStyle = dto.TextingStyle;
        
        if (!string.IsNullOrWhiteSpace(dto.Interests))
            data.Interests = dto.Interests;
        
        if (!string.IsNullOrWhiteSpace(dto.Example))
            data.Example = dto.Example;
    }

    private BotDetailsDto MakeDto(Bot bot)
    {
        var personalitydto = new PersonalityDataDto()
        {
            CoreIdentity = bot.PersonalityData.CoreIdentity,
            Dislikes = bot.PersonalityData.Dislikes,
            Likes = bot.PersonalityData.Likes,
            Interests = bot.PersonalityData.Interests,
            Personality = bot.PersonalityData.Personality,
            TextingStyle = bot.PersonalityData.TextingStyle,
            Example = bot.PersonalityData.Example
        };
        return new BotDetailsDto()
        {
            Id = bot.UserId,
            Name = bot.User.Name,
            PersonalityData = personalitydto
        };
    }
}
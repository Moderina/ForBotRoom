using System.Text.Json;
using BotChat.App.UserLogic;
using BotChat.Contracts.Bots;
using BotChat.Contracts.Storage;
using BotChat.Domain;
using BotChat.Domain.Bots;
using BotChat.Domain.Users;

namespace BotChat.App.BotLogic;

public class BotService : IBotService
{
    private readonly IUserRepository _userRepository;
    private readonly IBotRepository _botRepository;
    private readonly IFileStorage _fileStorage;

    public BotService(IBotRepository botRepository, IUserRepository userRepository, IFileStorage fileStorage)
    {
        _userRepository = userRepository;
        _botRepository = botRepository;
        _fileStorage = fileStorage;
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

    public Task<Bot?> GetBotByIdAsync(Guid id)
    {
        return _botRepository.GetBotByIdAsync(id);
    }

    public async Task<BotDetailsDto> GetBotDetailsAsync(Guid id)
    {
        var bot = await _botRepository.GetBotByIdAsync(id);

        var personalitydto = new PersonalityProfileDto()
        {
            CoreIdentity = bot.PersonalityProfile.CoreIdentity,
            Dislikes = bot.PersonalityProfile.Dislikes,
            Likes = bot.PersonalityProfile.Likes,
            Interests = bot.PersonalityProfile.Interests,
            Personality = bot.PersonalityProfile.Personality,
            TextingStyle = bot.PersonalityProfile.TextingStyle,
            Example = bot.PersonalityProfile.Example
        };
        return new BotDetailsDto()
        {
            Id = bot.UserId,
            Name = bot.User.Name,
            PersonalityProfile = personalitydto,
            ProfilePictureUrl = $"/api/media/profile-pictures/{bot.User.ProfilePictureUrl}",
        };
    }

    public async Task<BotDetailsDto> CreateBotAsync(string name, string personality, FileUpload? profilePicture, CancellationToken cancellationToken)
    {
        string profilePictureUrl = "";
        if (profilePicture != null)
        {
            profilePictureUrl = await _fileStorage.SaveProfilePictureAsync(profilePicture, cancellationToken);
        }
        var savedUser = await _userRepository.CreateUserAsync(new User(name, UserType.Bot, profilePictureUrl));
        
        var personalityData = JsonSerializer.Deserialize<PersonalityProfile>(personality, new JsonSerializerOptions{PropertyNameCaseInsensitive = true});
        var newbot =await _botRepository.AddBotAsync(new Bot(savedUser.Id, personalityData));
        return MakeDto(newbot);
    }

    public async Task<BotDetailsDto?> UpdateBotAsync(Guid id, string name, string personality, FileUpload? profilePicture, CancellationToken cancellationToken)
    {
        var savedBot = await _botRepository.GetBotByIdAsync(id);
        if (savedBot == null)
        {
            Console.WriteLine("Bot not found");
            return null;
        }
        
        if (!string.IsNullOrWhiteSpace(name))
            savedBot.User.Name = name;
        var personalityData = JsonSerializer.Deserialize<PersonalityProfile>(personality, new JsonSerializerOptions{PropertyNameCaseInsensitive = true});
        Console.WriteLine(personality);
        if (personalityData != null)
        {
            Console.WriteLine(personalityData.CoreIdentity);
            savedBot.PersonalityProfile = personalityData;
        }
        
        if (profilePicture != null)
        {
            string profilePictureUrl = await _fileStorage.SaveProfilePictureAsync(profilePicture, cancellationToken);
            savedBot.User.ProfilePictureUrl = profilePictureUrl;
        }
        await _botRepository.UpdateBotAsync(savedBot);
        
        return MakeDto(savedBot);
    }

    public Task DeleteBotAsync(Guid id)
    {
        return Task.CompletedTask;
    }
    
    private void MapPersonalityData(PersonalityProfile profile, PersonalityProfileDto dto)
    {
        if (!string.IsNullOrWhiteSpace(dto.Personality))
            profile.Personality = dto.Personality;

        if (!string.IsNullOrWhiteSpace(dto.Likes))
            profile.Likes = dto.Likes;

        if (!string.IsNullOrWhiteSpace(dto.Dislikes))
            profile.Dislikes = dto.Dislikes;
        
        if (!string.IsNullOrWhiteSpace(dto.CoreIdentity))
            profile.CoreIdentity = dto.CoreIdentity;
        
        if (!string.IsNullOrWhiteSpace(dto.TextingStyle))
            profile.TextingStyle = dto.TextingStyle;
        
        if (!string.IsNullOrWhiteSpace(dto.Interests))
            profile.Interests = dto.Interests;
        
        if (!string.IsNullOrWhiteSpace(dto.Example))
            profile.Example = dto.Example;
    }

    private BotDetailsDto MakeDto(Bot bot)
    {
        var personalitydto = new PersonalityProfileDto()
        {
            CoreIdentity = bot.PersonalityProfile.CoreIdentity,
            Dislikes = bot.PersonalityProfile.Dislikes,
            Likes = bot.PersonalityProfile.Likes,
            Interests = bot.PersonalityProfile.Interests,
            Personality = bot.PersonalityProfile.Personality,
            TextingStyle = bot.PersonalityProfile.TextingStyle,
            Example = bot.PersonalityProfile.Example
        };
        return new BotDetailsDto()
        {
            Id = bot.UserId,
            Name = bot.User.Name,
            PersonalityProfile = personalitydto
        };
    }
}
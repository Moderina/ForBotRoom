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

        var personalitydto = new PersonalityProfileDto();
        MapPersonalityData(bot.PersonalityProfile, personalitydto);
        
        return new BotDetailsDto()
        {
            Id = bot.UserId,
            Name = bot.User.Name,
            PersonalityProfile = personalitydto,
            ProfilePictureUrl = $"/api/media/profile-pictures/{bot.User.ProfilePictureUrl}",
        };
    }

    public async Task<BotDetailsDto> CreateBotAsync(string name, PersonalityProfile personalityProfile, FileUpload? profilePicture, CancellationToken cancellationToken)
    {
        string profilePictureUrl = "";
        if (profilePicture != null)
        {
            profilePictureUrl = await _fileStorage.SaveProfilePictureAsync(profilePicture, cancellationToken);
        }
        var savedUser = await _userRepository.CreateUserAsync(new User(name, UserType.Bot, profilePictureUrl));
        
        var newbot =await _botRepository.AddBotAsync(new Bot(savedUser.Id, personalityProfile));
        return MakeDto(newbot);
    }

    public async Task<BotDetailsDto?> UpdateBotAsync(Guid id, string name, PersonalityProfile? personalityProfile, FileUpload? profilePicture, CancellationToken cancellationToken)
    {
        var savedBot = await _botRepository.GetBotByIdAsync(id);
        if (savedBot == null)
        {
            Console.WriteLine("Bot not found");
            return null;
        }
        
        if (!string.IsNullOrWhiteSpace(name))
            savedBot.User.Name = name;

        if (personalityProfile != null)
        {
            Console.WriteLine(personalityProfile.CoreIdentity);
            savedBot.PersonalityProfile = personalityProfile;
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
    
    private void MapPersonalityData(PersonalityProfile personalityFrom, PersonalityProfileDto personalityTo)
    {
        if (!string.IsNullOrWhiteSpace(personalityFrom.Personality))
            personalityTo.Personality = personalityFrom.Personality;

        if (!string.IsNullOrWhiteSpace(personalityFrom.Likes))
            personalityTo.Likes = personalityFrom.Likes;

        if (!string.IsNullOrWhiteSpace(personalityFrom.Dislikes))
            personalityTo.Dislikes = personalityFrom.Dislikes;
        
        if (!string.IsNullOrWhiteSpace(personalityFrom.CoreIdentity))
            personalityTo.CoreIdentity = personalityFrom.CoreIdentity;
        
        if (!string.IsNullOrWhiteSpace(personalityFrom.TextingStyle))
            personalityTo.TextingStyle = personalityFrom.TextingStyle;
        
        if (!string.IsNullOrWhiteSpace(personalityFrom.Interests))
            personalityTo.Interests = personalityFrom.Interests;
        
        if (!string.IsNullOrWhiteSpace(personalityFrom.Example))
            personalityTo.Example = personalityFrom.Example;
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
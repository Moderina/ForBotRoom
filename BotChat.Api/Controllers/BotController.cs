using BotChat.App.BotLogic;
using BotChat.Contracts.Bots;
using Microsoft.AspNetCore.Mvc;

namespace BotChat.Api.Controllers;

[ApiController]
[Route("api/bots")]
public class BotController : ControllerBase
{
    private readonly IBotService _botService;
    private readonly IWebHostEnvironment _environment;
    public BotController(IBotService botService, IWebHostEnvironment environment)
    {
        _botService = botService;
        _environment = environment;
    }
    
    [HttpGet("getAll")]
    public async Task<ActionResult<IEnumerable<BotDto>>> GetAll()
    {
        var bots = await _botService.GetBotsAsync();
        
        return Ok(bots);
    }
    
    [HttpGet("{botId}/details")]
    public async Task<ActionResult<IEnumerable<BotDetailsDto>>> GetBotDetails(Guid botId)
    {
        var bot = await _botService.GetBotDetailsAsync(botId);
        
        return Ok(bot);
    }
    
    [HttpPost("")]
    public async Task<ActionResult<BotDetailsDto>> CreateNewBot([FromForm] CreateBotRequest request)
    {
        var profilePictureUrl = "";

        if (request.ProfilePicture != null)
        {
            profilePictureUrl = await SaveProfilePictureAsync(
                request.ProfilePicture);
        }
        var botdto = await _botService.CreateBotAsync(request.Name, request.PersonalityData, profilePictureUrl);
        
        return Ok(botdto);
    }
    
    private async Task<string> SaveProfilePictureAsync(
        IFormFile file)
    {
        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        var path = Path.Combine(
            _environment.WebRootPath,
            "uploads",
            "profile-pics",
            fileName);

        Directory.CreateDirectory(Path.GetDirectoryName(path)!);
        await using var stream = System.IO.File.Create(path);
        await file.CopyToAsync(stream);

        return $"/uploads/profile-pics/{fileName}";
    }
}
using BotChat.App.BotLogic;
using BotChat.Contracts.Bots;
using Microsoft.AspNetCore.Mvc;

namespace BotChat.Api.Controllers;

[ApiController]
[Route("api/bots")]
public class BotController : ControllerBase
{
    private readonly IBotService _botService;

    public BotController(IBotService botService)
    {
        _botService = botService;
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
    
    [HttpPost("create")]
    public async Task<ActionResult<BotDetailsDto>> CreateNewBot([FromBody] CreateBotRequest request)
    {
        var botdto = await _botService.CreateBotAsync(request);
        
        return Ok(botdto);
    }
}
using BotChat.App.ChatLogic;
using BotChat.Contracts.Chat;
using Microsoft.AspNetCore.Mvc;

namespace BotChat.Api.Controllers;

[ApiController]
[Route("api/chat")]
public class ChatController : ControllerBase
{
    private readonly IChatService _chatService;
    
    public ChatController(IChatService chatService)
    {
        _chatService = chatService;
    }
    
    [HttpGet("getAll")]
    public async Task<ActionResult<ChatDto>> GetAllChats()
    {
        var chats = _chatService.GetChats();
        
        return Ok(chats);
    }
    
    [HttpPost("new")]
    public async Task<ActionResult<ChatDto>> CreateNewChat([FromBody] CreateChatRequest request)
    {
        var authHeader = Request.Headers.Authorization.ToString();
        var token = authHeader.Replace("Bearer ", "");
        var userId = Guid.Parse(token);
        
        var chat = _chatService.CreateChat(userId, request);
        
        return Ok(chat);
    }
}
using BotChat.App.ChatLogic;
using BotChat.Contracts.Chat;
using Microsoft.AspNetCore.Mvc;

namespace BotChat.Api.Controllers;

[ApiController]
[Route("api/chats")]
public class ChatController : ControllerBase
{
    private readonly IChatService _chatService;
    private readonly IMessageService _messageService;
    
    public ChatController(IChatService chatService)
    {
        _chatService = chatService;
        _messageService = messageService;
    }
    
    [HttpGet("getAll")]
    public async Task<ActionResult<ChatDto>> GetAllChats()
    {
        var chats = await _chatService.GetChatsAsync();
        
        return Ok(chats);
    }
    
    [HttpPost("new")]
    public async Task<ActionResult<ChatDto>> CreateNewChat([FromBody] CreateChatRequest request)
    {
        var authHeader = Request.Headers.Authorization.ToString();
        var token = authHeader.Replace("Bearer ", "");
        var userId = Guid.Parse(token);
        
        var chat = await _chatService.CreateChatAsync(userId, request);
        
        return Ok(chat);
    }
    
    [HttpPost("{chatId}/messages")]
    public async Task<ActionResult<MessageDto>> CreateMessage(Guid chatId, CreateMessageRequest request)
    {
        var authHeader = Request.Headers.Authorization.ToString();
        var token = authHeader.Replace("Bearer ", "");
        var userId = Guid.Parse(token);
        
        var message = await _messageService.CreateMessageAsync(chatId, userId, request.Content);
        
        // await _hub.Clients
        //     .Group(message.ChatId.ToString())
        //     .SendAsync("ReceiveMessage", message);
        return Ok(message);
    }
}
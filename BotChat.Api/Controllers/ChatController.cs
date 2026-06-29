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
    
    public ChatController(IChatService chatService, IMessageService messageService)
    {
        _chatService = chatService;
        _messageService = messageService;
    }
    
    [HttpGet]
    public async Task<ActionResult<ChatDto>> GetChats([FromQuery] bool active = true)
    {
        var chats = await _chatService.GetActiveChatsAsync();
        
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
    
    [HttpPost("{chatid}/disable")]
    public async Task<ActionResult<ChatDto>> DisableChat(Guid chatId)
    {
        await _chatService.DisableChatAsync(chatId);
        
        return NoContent();
    }
    
    [HttpGet("{chatId}/messages")]
    public async Task<ActionResult<MessageDto>> GetMessageHistory(Guid chatId, [FromQuery] GetMessageHistoryRequest request)
    {
        var amount = request.Amount ?? 20;
        var lastMessageTime = request.Before ?? -1;
        var messages = await _messageService.GetChatHistoryAsync(chatId, amount, lastMessageTime);
        
        return Ok(messages);
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
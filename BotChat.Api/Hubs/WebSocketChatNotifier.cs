using BotChat.Api.Hubs;
using BotChat.App.ChatLogic;
using BotChat.App.ConversationLogic;
using BotChat.Contracts.Chat;
using Microsoft.AspNetCore.SignalR;

namespace BotChat.Api.Controllers;

public class WebSocketChatNotifier : IChatNotifier
{
    private readonly IHubContext<ChatHub> _hub;
    private readonly IChatService _chatService;

    public WebSocketChatNotifier(IHubContext<ChatHub> hub, IChatService chatService)
    {
        _hub = hub;
        _chatService = chatService;
    }

    public async Task SendMessageAsync(MessageDto message, List<Guid> userIds)
    {
        Console.WriteLine($"Sending message to clients");
        Console.WriteLine($"Found {userIds.Count} users of this chat");
        foreach (var id in userIds)
        {
            Console.WriteLine($"Sending message to {id}");
            await _hub.Clients
                .Group(id.ToString())
                .SendAsync(
                    "ReceiveMessage",
                    message);
        }
    }
}
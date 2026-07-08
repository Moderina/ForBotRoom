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

    public async Task SendMessageAsync(MessageDto message)
    {
        Console.WriteLine($"Sending message to clients");
        var members = await _chatService.GetHumanMembersOfChatAsync(message.ChatId);
        Console.WriteLine($"Found {members.Count} users of this chat");
        foreach (var user in members)
        {
            Console.WriteLine($"Sending message to {user.Id}");
            await _hub.Clients
                .Group(user.Id.ToString())
                .SendAsync(
                    "ReceiveMessage",
                    message);
        }
    }
}
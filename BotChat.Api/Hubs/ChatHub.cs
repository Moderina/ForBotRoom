using Microsoft.AspNetCore.SignalR;

namespace BotChat.Api.Hubs;

public class ChatHub : Hub
{
    public async Task RegisterUser(Guid userId)
    {
        Console.WriteLine($"Registering user {userId}");
        await Groups.AddToGroupAsync(Context.ConnectionId, userId.ToString());
    }

    public async Task LeaveChat(Guid chatId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatId.ToString());
    }
}
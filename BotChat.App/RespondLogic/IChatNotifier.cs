using BotChat.Contracts.Chat;

namespace BotChat.App.RespondLogic;

public interface IChatNotifier
{
    Task SendMessageAsync(MessageDto message, List<Guid> userIds);
}
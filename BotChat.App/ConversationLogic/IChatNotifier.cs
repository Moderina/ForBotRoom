using BotChat.Contracts.Chat;

namespace BotChat.App.ConversationLogic;

public interface IChatNotifier
{
    Task SendMessageAsync(MessageDto message, List<Guid> userIds);
}
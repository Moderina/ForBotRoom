using BotChat.Contracts.Chat;
using BotChat.Domain.Bots;
using BotChat.Domain.Chats;

namespace BotChat.App.ConversationLogic;

public interface IConversationService
{
    Task<MessageDto> HandleUserMessageAsync(Guid chatId, Guid userId, string message);
}
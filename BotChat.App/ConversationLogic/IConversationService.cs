namespace BotChat.App.ConversationLogic;

public interface IConversationService
{
    Task GenerateBotResponseAsync(ConversationJob conversationJob);
}
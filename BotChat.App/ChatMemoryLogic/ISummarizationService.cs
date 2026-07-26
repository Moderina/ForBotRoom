namespace BotChat.App.ConversationLogic;

public interface ISummarizationService
{
    public Task GenerateChatSummaryAsync(SummarizeChatJob job);
}
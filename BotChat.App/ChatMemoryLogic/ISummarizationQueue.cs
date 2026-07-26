namespace BotChat.App.ConversationLogic;

public interface ISummarizationQueue
{
    ValueTask QueueAsync(SummarizeChatJob job);
    ValueTask<SummarizeChatJob> DequeueAsync(CancellationToken ct);
}

public record SummarizeChatJob(
    Guid ChatId,
    int SummaryMessagesBatch
);
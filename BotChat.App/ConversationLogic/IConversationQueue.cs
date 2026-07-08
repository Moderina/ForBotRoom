namespace BotChat.App.ConversationLogic;

public interface IConversationQueue
{
    ValueTask QueueAsync(ConversationJob job);
    ValueTask<ConversationJob> DequeueAsync(CancellationToken ct);
}

public record ConversationJob(
    Guid ChatId
);
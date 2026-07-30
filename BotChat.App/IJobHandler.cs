namespace BotChat.App;

public interface IJobHandler<TJob>
{
    Task HandleAsync(TJob job, Action releaseLlmSlot, CancellationToken ct);
}
namespace BotChat.App;

public interface IJobQueue<TJob>
{
    ValueTask QueueAsync(TJob job);
    ValueTask<TJob> DequeueAsync(CancellationToken ct);
}
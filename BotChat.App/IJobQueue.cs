namespace BotChat.App;

public interface IJobQueue<TJob>
{
    ValueTask QueueAsync(TJob job);
    ValueTask<TJob> DequeueAsync(CancellationToken ct);
    bool TryDequeue(out TJob job);  
    ValueTask<bool> WaitToReadAsync(CancellationToken ct);
}
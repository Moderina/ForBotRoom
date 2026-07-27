namespace BotChat.App;

public interface IJobHandler<TJob>
{
    Task HandleAsync(TJob job, CancellationToken ct);
}
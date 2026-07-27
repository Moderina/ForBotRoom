using System.Threading.Channels;
using BotChat.App;

namespace BotChat.Infrastructure.Background;

public class JobQueue<TJob> : IJobQueue<TJob>
{
    private readonly Channel<TJob> _queue = Channel.CreateUnbounded<TJob>();

    public async ValueTask QueueAsync(TJob job) => await _queue.Writer.WriteAsync(job);

    public async ValueTask<TJob> DequeueAsync(CancellationToken ct) =>
        await _queue.Reader.ReadAsync(ct);
}
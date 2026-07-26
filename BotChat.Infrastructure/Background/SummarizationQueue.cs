using System.Threading.Channels;

namespace BotChat.App.ConversationLogic;

public class SummarizationQueue : ISummarizationQueue
{
    private readonly Channel<SummarizeChatJob> _queue;

    public SummarizationQueue()
    {
        _queue = Channel.CreateUnbounded<SummarizeChatJob>();
    }

    public async ValueTask QueueAsync(SummarizeChatJob job)
    {
        await _queue.Writer.WriteAsync(job);
    }

    public async ValueTask<SummarizeChatJob> DequeueAsync(
        CancellationToken cancellationToken)
    {
        return await _queue.Reader.ReadAsync(cancellationToken);
    }
}
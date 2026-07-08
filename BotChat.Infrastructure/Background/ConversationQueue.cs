using System.Threading.Channels;

namespace BotChat.App.ConversationLogic;

public class ConversationQueue : IConversationQueue
{
    private readonly Channel<ConversationJob> _queue;

    public ConversationQueue()
    {
        _queue = Channel.CreateUnbounded<ConversationJob>();
    }

    public async ValueTask QueueAsync(ConversationJob job)
    {
        await _queue.Writer.WriteAsync(job);
    }

    public async ValueTask<ConversationJob> DequeueAsync(
        CancellationToken cancellationToken)
    {
        return await _queue.Reader.ReadAsync(cancellationToken);
    }
}
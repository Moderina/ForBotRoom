using BotChat.App;
using BotChat.App.ChatMemoryLogic;
using BotChat.App.RespondLogic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BotChat.Infrastructure.Background;

public class LlmDispatchWorker : BackgroundService
{
    private readonly IJobQueue<RespondJob> _responseQueue;
    private readonly IJobQueue<SummarizeChatJob> _summarizeQueue;
    private readonly IServiceScopeFactory _scopeFactory;
    
    public LlmDispatchWorker(
        IJobQueue<RespondJob> responseQueue,
        IJobQueue<SummarizeChatJob> summarizeQueue,
        IServiceScopeFactory scopeFactory)
    {
        _responseQueue = responseQueue;
        _summarizeQueue = summarizeQueue;
        _scopeFactory = scopeFactory;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            // 1. Always try the high-priority (response) queue first, non-blocking.
            if (_responseQueue.TryDequeue(out var responseJob))
            {
                await DispatchAsync(responseJob, stoppingToken);
                continue; // loop again, re-check response queue before touching summarization
            }

            // 2. No response job waiting — check summarization, still non-blocking.
            if (_summarizeQueue.TryDequeue(out var summarizeJob))
            {
                await DispatchAsync(summarizeJob, stoppingToken);
                continue;
            }

            // 3. Nothing ready in either queue — wait efficiently for whichever arrives first.
            var responseWait = _responseQueue.WaitToReadAsync(stoppingToken).AsTask();
            var summarizeWait = _summarizeQueue.WaitToReadAsync(stoppingToken).AsTask();
            await Task.WhenAny(responseWait, summarizeWait);
            // loop back around — step 1 re-checks response queue regardless of which one woke us
        }
    }

    private async Task DispatchAsync<TJob>(TJob job, CancellationToken ct)
    {
        using var scope = _scopeFactory.CreateScope();
        var handler = scope.ServiceProvider.GetRequiredService<IJobHandler<TJob>>();
        
        var slotReleased = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);

        var handlerTask = Task.Run(async () =>
        {
            try
            {
                await handler.HandleAsync(job, () => slotReleased.TrySetResult(), ct);
            }
            catch (Exception ex)
            {
                Console.Write(ex + ": Job {JobType} failed: " + typeof(TJob).Name);
            }
            finally
            {
                slotReleased
                    .TrySetResult(); // safety net: release even if handler never called it (early return, exception before LLM call, etc.)
                scope.Dispose();
            }
        }, ct);
        
        await slotReleased.Task;
    }
}
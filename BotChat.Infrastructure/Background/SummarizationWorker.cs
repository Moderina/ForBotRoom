using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BotChat.App.ConversationLogic;

public class SummarizationWorker : BackgroundService
{
    private readonly ISummarizationQueue _queue;
    private readonly IServiceScopeFactory _scopeFactory;
    
    public SummarizationWorker(ISummarizationQueue queue, IServiceScopeFactory scopeFactory)
    {
        _queue = queue;
        _scopeFactory = scopeFactory;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var job = await _queue.DequeueAsync(stoppingToken);
            
            using var scope = _scopeFactory.CreateScope();
            var conversationService = scope.ServiceProvider.GetRequiredService<ISummarizationService>();

            await conversationService.GenerateChatSummaryAsync(job);
        }
    }
}
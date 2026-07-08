using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BotChat.App.ConversationLogic;

public class ConversationWorker : BackgroundService
{
    private readonly IConversationQueue _queue;
    private readonly IServiceScopeFactory _scopeFactory;
    
    public ConversationWorker(IConversationQueue queue, IServiceScopeFactory scopeFactory)
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
            var conversationService = scope.ServiceProvider.GetRequiredService<IConversationService>();

            await conversationService.GenerateBotResponseAsync(job);
        }
    }
}
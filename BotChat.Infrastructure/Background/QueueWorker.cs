using BotChat.App;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BotChat.Infrastructure.Background;

public class QueueWorker<TJob> : BackgroundService
{
    private readonly IJobQueue<TJob> _queue;
    private readonly IServiceScopeFactory _scopeFactory;

    public QueueWorker(IJobQueue<TJob> queue, IServiceScopeFactory scopeFactory)
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
            var handler = scope.ServiceProvider.GetRequiredService<IJobHandler<TJob>>();

            try
            {
                await handler.HandleAsync(job, stoppingToken);
            }
            catch (Exception ex)
            {
                // var logger = scope.ServiceProvider.GetRequiredService<ILogger<QueueWorker<TJob>>>();
                // logger.LogError(ex, "Job {JobType} failed", typeof(TJob).Name);
                Console.WriteLine(ex);
            }
        }
    }
}
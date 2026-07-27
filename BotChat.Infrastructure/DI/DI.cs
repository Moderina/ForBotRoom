using BotChat.App;
using BotChat.App.BotLogic;
using BotChat.App.ChatLogic;
using BotChat.App.ChatMemoryLogic;
using BotChat.App.Config;
using BotChat.App.MessageLogic;
using BotChat.App.RespondLogic;
using BotChat.App.Storage;
using BotChat.App.UserLogic;
using BotChat.Infrastructure.Background;
using BotChat.Infrastructure.Persistant;
using BotChat.Infrastructure.Persistant.Repositories;
using BotChat.Infrastructure.Persistant.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BotChat.Infrastructure.DI;

public static class DI
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IChatRepository, ChatRepository>();
        services.AddScoped<IMessageRepository, MessageRepository>();
        services.AddScoped<IBotRepository, BotRepository>();
        services.AddScoped<IChatMemberRepository, ChatMemberRepository>();
        services.AddScoped<IChatMemoryRepository, ChatMemoryRepository>();
        
        services.AddSingleton<IAppDataPath, AppDataPath>();
        services.AddScoped<IFileStorage, LocalFileStorage>();
        
        services.AddSingleton<IJobQueue<RespondJob>, JobQueue<RespondJob>>();
        services.AddSingleton<IJobQueue<SummarizeChatJob>, JobQueue<SummarizeChatJob>>();
        services.AddHostedService<QueueWorker<RespondJob>>();
        services.AddHostedService<QueueWorker<SummarizeChatJob>>();
        
        var dbSettings = configuration.GetSection("Database").Get<DBSettings>();
        
        services.AddDbContext<AppDbContext>((serviceProvider, options) =>
        {
            var paths = serviceProvider.GetRequiredService<IAppDataPath>();
            var dbDirectory = Path.GetDirectoryName(paths.DatabaseDirectory);

            if (dbDirectory != null) {
                Directory.CreateDirectory(dbDirectory);
            }
            var dbPth = Path.Combine(paths.DatabaseDirectory, dbSettings.FileName);

            options.UseSqlite($"Data Source={dbPth}");
        });
        
        return services;
    }
}
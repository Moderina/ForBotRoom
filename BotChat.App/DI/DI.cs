using BotChat.App.BotLogic;
using BotChat.App.ChatLogic;
using BotChat.App.ChatMemoryLogic;
using BotChat.App.MessageLogic;
using BotChat.App.RespondLogic;
using BotChat.App.UserLogic;
using Microsoft.Extensions.DependencyInjection;

namespace BotChat.App.DI;

public static class DI
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IChatService, ChatService>();
        services.AddScoped<IMessageService, MessageService>();
        services.AddScoped<IBotService, BotService>();
        services.AddScoped<IJobHandler<RespondJob>, RespondJobHandler>();
        services.AddScoped<IJobHandler<SummarizeChatJob>, SummarizeChatJobHandler>();
        services.AddScoped<IChatMemoryService, ChatMemoryService>();
        return services;
    }
}
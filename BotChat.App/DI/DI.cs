using BotChat.App.BotLogic;
using BotChat.App.ChatLogic;
using BotChat.App.ConversationLogic;
using BotChat.App.Services;
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
        services.AddScoped<IConversationService, ConversationService>();
        services.AddScoped<IChatMemoryService, ChatMemoryService>();
        services.AddScoped<ISummarizationService, SummarizationService>();
        return services;
    }
}
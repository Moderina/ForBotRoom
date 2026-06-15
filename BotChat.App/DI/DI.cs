using Microsoft.Extensions.DependencyInjection;

namespace BotChat.App.DI;

public static class DI
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services;
    }
}
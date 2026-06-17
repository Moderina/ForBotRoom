using BotChat.App.Config;
using BotChat.Infrastructure.Persistant;
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
        var dbSettings = configuration.GetSection("Database").Get<DBSettings>();
        // Console.WriteLine($"Connection string: {dbSettings.ConnectionString}");
        
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(dbSettings.ConnectionString));
        
        return services;
    }
}
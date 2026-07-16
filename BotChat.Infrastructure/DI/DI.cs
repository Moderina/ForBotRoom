using BotChat.App;
using BotChat.App.Config;
using BotChat.Infrastructure.Persistant;
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
        var dbSettings = configuration.GetSection("Database").Get<DBSettings>();
        // Console.WriteLine($"Connection string: {dbSettings.ConnectionString}");
        
        services.AddDbContext<AppDbContext>((serviceProvider, options) =>
        {
            var paths = serviceProvider.GetRequiredService<IAppDataPath>();

            var dbDirectory = Path.GetDirectoryName(paths.DatabaseDirectory);

            if (dbDirectory != null)
            {
                Directory.CreateDirectory(dbDirectory);
            }
            
            var dbPth = Path.Combine(paths.DatabaseDirectory, dbSettings.FileName);

            options.UseSqlite(
                $"Data Source={dbPth}");
        });
        
        services.AddScoped<IFileStorage, LocalFileStorage>();
        
        return services;
    }
}
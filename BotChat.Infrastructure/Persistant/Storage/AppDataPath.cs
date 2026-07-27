using BotChat.App.Storage;

namespace BotChat.Infrastructure.Persistant.Storage;

public class AppDataPath : IAppDataPath
{
    public string Root { get; }

    public string DatabaseDirectory =>
        Path.Combine(Root, "Database");

    public string MediaDirectory =>
        Path.Combine(Root, "Media");


    public AppDataPath()
    {
        Root = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "ForBotRoom");
    }
}
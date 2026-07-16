namespace BotChat.App;

public interface IAppDataPath
{
    string Root { get; }
    string DatabaseDirectory { get; }
    string MediaDirectory { get; }
}
namespace BotChat.App.Config;

public class DBSettings
{
    public const string SectionName = "Database";

    public string ConnectionString { get; set; } = string.Empty;
}
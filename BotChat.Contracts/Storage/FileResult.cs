namespace BotChat.Contracts.Storage;

public class FileResult
{
    public required Stream Content { get; init; }
    public required string ContentType { get; init; }
}
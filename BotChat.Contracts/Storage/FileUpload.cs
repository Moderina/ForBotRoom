namespace BotChat.Contracts.Storage;

public class FileUpload
{
    public required Stream Content { get; init; }
    public required string FileName { get; init; }
    public required string ContentType { get; init; }
}
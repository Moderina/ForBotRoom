using BotChat.App.Storage;
using BotChat.Contracts.Storage;
using Microsoft.Extensions.Configuration;

namespace BotChat.Infrastructure.Persistant.Storage;

public class LocalFileStorage : IFileStorage
{
    private readonly string _storagePath;
    private readonly string _profilePicturesPath = "Media/ProfilePictures";

    public LocalFileStorage(IConfiguration configuration)
    {
        _storagePath = Path.Combine(
            Environment.GetFolderPath(
                Environment.SpecialFolder.LocalApplicationData),
            "ForBotRoom");
    }

    public async Task<string> SaveAsync(FileUpload file, string folder, CancellationToken cancellationToken = default)
    {
        var directory = Path.Combine(_storagePath, folder);

        Directory.CreateDirectory(directory);
        
        var extension = Path.GetExtension(file.FileName);
        var fileName = $"{Guid.NewGuid()}{extension}";
        var path = Path.Combine(directory, fileName);
        
        await using var stream = new FileStream(path, FileMode.Create);
        
        await file.Content.CopyToAsync(stream, cancellationToken);

        return Path.Combine(folder, fileName);
    }

    public async Task<string> SaveProfilePictureAsync(FileUpload file, CancellationToken cancellationToken = default)
    {
        var directory = Path.Combine(_storagePath, _profilePicturesPath);
        Directory.CreateDirectory(directory);
        
        var extension = Path.GetExtension(file.FileName);
        var fileName = $"{Guid.NewGuid()}{extension}";
        var path = Path.Combine(directory, fileName);
        Console.WriteLine(Path.GetFullPath(directory));
        
        await using var stream = new FileStream(path, FileMode.Create);
        
        await file.Content.CopyToAsync(stream, cancellationToken);

        return Path.Combine(fileName);
    }

    public async Task<FileResult> GetProfilePictureAsync(string fileName, CancellationToken cancellationToken)
    {
        var safeFileName = Path.GetFileName(fileName);
        var path = Path.Combine(_storagePath, _profilePicturesPath, safeFileName);
        
        if (!File.Exists(path))
            return null;
        
        var stream = new FileStream(
            path,
            FileMode.Open,
            FileAccess.Read,
            FileShare.Read);
        
        var contentType = GetContentType(path);
        
        return new FileResult()
        {
            Content = stream,
            ContentType = contentType
        };
    }
    
    private static string GetContentType(string path)
    {
        var extension = Path.GetExtension(path).ToLowerInvariant();

        return extension switch
        {
            ".jpg" or ".jpeg" => "image/jpeg",
            ".png" => "image/png",
            ".gif" => "image/gif",
            ".webp" => "image/webp",

            _ => "application/octet-stream"
        };
    }
}
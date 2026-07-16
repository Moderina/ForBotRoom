using BotChat.App;
using BotChat.Contracts.Storage;
using Microsoft.Extensions.Configuration;

namespace BotChat.Infrastructure.Persistant.Storage;

public class LocalFileStorage : IFileStorage
{
    private readonly string _storagePath;

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
        var storageFolder = "Media/ProfilePictures";
        var directory = Path.Combine(_storagePath, storageFolder);
        Directory.CreateDirectory(directory);
        
        var extension = Path.GetExtension(file.FileName);
        var fileName = $"{Guid.NewGuid()}{extension}";
        var path = Path.Combine(directory, fileName);
        Console.WriteLine(Path.GetFullPath(directory));
        
        await using var stream = new FileStream(path, FileMode.Create);
        
        await file.Content.CopyToAsync(stream, cancellationToken);

        return Path.Combine(storageFolder, fileName);
    }
}
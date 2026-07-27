using BotChat.Contracts.Storage;

namespace BotChat.App.Storage;

public interface IFileStorage
{
    Task<string> SaveAsync(
        FileUpload file,
        string folder,
        CancellationToken cancellationToken = default);
    
    Task<string> SaveProfilePictureAsync(
        FileUpload file,
        CancellationToken cancellationToken = default);

    Task<FileResult> GetProfilePictureAsync(string fileName, CancellationToken cancellationToken);
}
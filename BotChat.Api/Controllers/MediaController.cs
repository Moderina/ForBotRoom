using BotChat.App.Storage;
using Microsoft.AspNetCore.Mvc;

namespace BotChat.Api.Controllers;

[ApiController]
[Route("api/media")]
public class MediaController : ControllerBase
{
    private readonly IFileStorage _fileStorage;

    public MediaController(IFileStorage fileStorage)
    {
        _fileStorage = fileStorage;
    }

    [HttpGet("profile-pictures/{fileName}")]
    public async Task<IActionResult> GetProfilePicture(string fileName, CancellationToken cancellationToken)
    {
        var file = await _fileStorage.GetProfilePictureAsync(fileName, cancellationToken);

        if (file == null)
            return NotFound();
        
        return File(file.Content, file.ContentType);
    }
}
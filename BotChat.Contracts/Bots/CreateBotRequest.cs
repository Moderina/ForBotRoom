using Microsoft.AspNetCore.Http;

namespace BotChat.Contracts.Bots;

public class CreateBotRequest
{    
    public string Name { get; set; } = string.Empty;
    public string PersonalityData { get; set; } = "";
    public IFormFile? ProfilePicture { get; set; }
}
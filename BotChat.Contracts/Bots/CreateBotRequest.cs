namespace BotChat.Contracts.Bots;

public class CreateBotRequest
{    
    public string Name { get; set; } = string.Empty;
    public PersonalityDataDto PersonalityData { get; set; } = new();
}
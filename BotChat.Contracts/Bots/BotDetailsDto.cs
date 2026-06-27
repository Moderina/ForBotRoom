namespace BotChat.Contracts.Bots;

public class BotDetailsDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } 
    public PersonalityDataDto PersonalityData { get; set; } 
}
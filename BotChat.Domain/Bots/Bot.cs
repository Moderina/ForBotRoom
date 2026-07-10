namespace BotChat.Domain.Bots;

public class Bot
{
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
    public PersonalityData PersonalityData { get; set; }
    public Mood Mood { get; }
    public bool IsActive { get; set; } = true;
    
    public Bot() {}

    public Bot(Guid userId, PersonalityData personalityData)
    {
        UserId = userId;
        PersonalityData = personalityData;
        Mood = new Mood("Calm", 20, 80);
    }
}
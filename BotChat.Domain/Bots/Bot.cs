namespace BotChat.Domain.Bots;

public class Bot
{
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
    public PersonalityProfile PersonalityProfile { get; set; }
    public Mood Mood { get; }
    public bool IsActive { get; set; } = true;
    
    public Bot() {}

    public Bot(Guid userId, PersonalityProfile personalityProfile)
    {
        UserId = userId;
        PersonalityProfile = personalityProfile;
        Mood = new Mood("Calm", 20, 80);
    }
}
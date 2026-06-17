namespace BotChat.Domain.Agents;

public class Bot
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public PersonalityData PersonalityData { get; set; }
    public Mood Mood { get; }
    public bool IsActive { get; set; } = true;

    // public AgentMemory Memory { get; set; }
}
namespace BotChat.Domain.Bots;

public class Mood
{
    public string CurrentMood { get; set; }
    public int Intensity { get; set; }
    public int Energy { get; set; }

    public Mood(string currentMood, int intensity, int energy)
    {
        CurrentMood = currentMood;
        Intensity = intensity;
        Energy = energy;
    }
}
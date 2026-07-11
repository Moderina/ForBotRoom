namespace BotChat.Domain.Llm;

public class LlmPrompt
{
    public string Prompt { get; set; }
    public string[] Stop {get; set;}

    public LlmPrompt(string prompt, string[] stop)
    {
        Prompt = prompt;
        Stop = stop;
    }
}
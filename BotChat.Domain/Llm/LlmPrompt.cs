namespace BotChat.Domain.Llm;

public class LlmPrompt
{
    public string Prompt { get; set; }
    
    public string Grammar { get; set; }
    public string[] Stop {get; set;}

    public LlmPrompt(string prompt, string grammar, string[] stop)
    {
        Prompt = prompt;
        Grammar = grammar;
        Stop = stop;
    }
}
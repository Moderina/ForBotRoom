namespace BotChat.Domain.Llm;

public class LlmMessageTo
{
    public string Role { get; init; } = "";
    public string Content { get; init; } = "";

    public LlmMessageTo(string role, string content)
    {
        Role = role;
        Content = content;
    }
}
using System.Text.Json.Serialization;

namespace BotChat.Domain.Llm;

public class LlmResponse
{
    [JsonPropertyName("response")]
    public string Response { get; set; }

    [JsonPropertyName("self_memory")]
    public string SelfMemory { get; set; }

    public LlmResponse(string response, string selfMemory)
    {
        Response = response;
        SelfMemory = selfMemory;
    }
}

public class LlmChoice
{
    [JsonPropertyName("message")]
    public LlmMessage Message { get; set; }
}

public class LlmMessage
{
    [JsonPropertyName("role")]
    public string Role { get; set; }
    [JsonPropertyName("content")]
    public string Content { get; set; }
}

public class LlmRawResponse
{
    [JsonPropertyName("choices")]
    public LlmChoice[] Choices { get; set; }
}
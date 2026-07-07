using System.Text.Json;
using BotChat.Domain.Llm;

namespace BotChat.App.LlmLogic;

public static class LlmParser
{
    public static LlmResponse ParseResponse(string rawJson)
    {
        string? contentString = null;
        try 
        {
            var apiResponse = JsonSerializer.Deserialize<LlmRawResponse>(rawJson);

            if (apiResponse?.Choices is not { Length: > 0 })
                throw new InvalidOperationException("Empty API response");

            contentString = apiResponse.Choices[0].Message.Content;

            if (string.IsNullOrWhiteSpace(contentString))
                throw new InvalidOperationException("AI returned empty content");

            var payload = JsonSerializer.Deserialize<LlmResponse>(contentString);

            if (payload == null)
                throw new InvalidOperationException("Failed to parse AI payload");

            return new LlmResponse(payload.Response, payload.SelfMemory);
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"Parse Error: {ex.Message}");
            Console.WriteLine($"Raw Content: {contentString}");
            throw;
        }
    }
}
using System.Net.Http.Json;
using System.Text.Json;
using BotChat.Domain;
using BotChat.Domain.Llm;

namespace BotChat.App.LlmLogic;

public class LlamaCppService : ILlmService
{
    private readonly HttpClient _http;

    public LlamaCppService(HttpClient http)
    {
        _http = http;
    }

    public async Task<LlmResponse?> GenerateAsync(PromptContent prompt)
    {
        try
        {
            var request = new
            {
                model = "stheno",
                messages = new[]
                {
                    new { role = "system", content = prompt.SystemPrompt},
                    new { role = "user", content = prompt.UserPrompt }
                },
                temperature = 0.7,
                max_tokens = 500
            };

            var response = await _http.PostAsJsonAsync(
                "/v1/chat/completions",
                // "/api/chat",
                request);

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            Console.WriteLine(json);
            // var responseString = json.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString()!.Trim();
            // Console.WriteLine("RESPONSE: |" + responseString + "|");
            return LlmParser.ParseResponse(json);
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine("LLM connection error: " + ex.Message);
        }
        catch (TaskCanceledException ex)
        {
            Console.WriteLine("LLM timeout: " + ex.Message);
        }
        catch (JsonException ex)
        {
            Console.WriteLine("LLM bad json: " + ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine("LLM unknown error: " + ex);
        }
        return null;
    }
}
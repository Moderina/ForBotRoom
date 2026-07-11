using System.Net.Http.Json;
using System.Text.Json;
using BotChat.Domain.Llm;

namespace BotChat.App.LlmLogic;

public class LlamaCppService : ILlmService
{
    private readonly HttpClient _http;

    public LlamaCppService(HttpClient http)
    {
        _http = http;
    }

    public async Task<string> GenerateAsync(List<LlmMessageTo> llmMessages)
    {
        try
        {
            var request = new
            {
                model = "stheno",
                messages = llmMessages,
                temperature = 0.7,
                max_tokens = 500,
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
    
    public async Task<string> GenerateAsyncTEST(LlmPrompt llmPrompt)
    {
        try
        {
            var request = new
            {
                prompt = llmPrompt.Prompt,
                stop = llmPrompt.Stop,
                temperature = 0.7,
                top_p = 0.9,
                repeat_penalty = 1.1,
                n_predict = 200,
                cache_prompt = true // cache last prompt -> faster recompute
            };

            var response = await _http.PostAsJsonAsync(
                "/completion",
                request);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<LlmCompletionResponse>();
            Console.WriteLine(result);
            return result.Content.Trim();
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
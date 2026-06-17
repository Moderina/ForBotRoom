using System.Text.Json;

namespace BotChat.Infrastructure.Helpers;

public static class JsonHelper
{
    public static JsonSerializerOptions ReceiveOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public static JsonSerializerOptions CamelCase = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };
}
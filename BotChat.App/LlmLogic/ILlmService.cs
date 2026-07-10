using BotChat.Domain;
using BotChat.Domain.Llm;

namespace BotChat.App.LlmLogic;

public interface ILlmService
{
    Task<string> GenerateAsync(List<LlmMessageTo> llmMessages);
}
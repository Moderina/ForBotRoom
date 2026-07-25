using BotChat.App.ChatLogic;
using BotChat.App.LlmLogic;
using BotChat.Domain.Chats;

namespace BotChat.App.ConversationLogic;

public class ChatMemoryService : IChatMemoryService
{
    
    private readonly IChatMemoryRepository _chatMemoryRepository;
    private readonly ILlmService _llmService;

    public ChatMemoryService(IChatMemoryRepository chatMemoryRepository, ILlmService llmService)
    {
        _chatMemoryRepository = chatMemoryRepository;
        _llmService = llmService;
    }

    public async Task<ChatMemory> GetChatMemory(Guid chatId)
    {
        return await _chatMemoryRepository.GetChatMemoryByIdAsync(chatId) ?? new ChatMemory() {ChatId = chatId};
    }

    public async Task<string?> GetSummaryForPromptAsync(Guid chatId)
    {
        return "";
    }

    public async Task<ChatMemory> UpdateMemoryAsync(ChatMemory memory, List<Message> messages)
    {
        var prompt = PromptBuilder.BuildPrompt_SummarizeChat(memory.Summary, messages);
        memory.Summary = await _llmService.GenerateAsyncTEST(prompt);
        Console.WriteLine("New memory sumamary:" + memory.Summary);
        memory.LastSummarizedMessageId = messages.Last().Id;
        memory.LastSummarizedAt = messages.Last().Timestamp;
        memory.UpdatedAt = DateTime.UtcNow;
        await _chatMemoryRepository.UpsertAsync(memory);
        return memory;
    }
}
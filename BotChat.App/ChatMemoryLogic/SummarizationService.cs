using BotChat.App.ChatLogic;
using BotChat.App.LlmLogic;

namespace BotChat.App.ConversationLogic;

public class SummarizationService : ISummarizationService
{
    private const int SummarizeBatchSize = 40;
    
    private readonly IChatMemoryService _chatMemoryService;
    private readonly IMessageService _messageService;
    private readonly ILlmService _llmService;

    public SummarizationService(IChatMemoryService chatMemoryService, IMessageService messageService, ILlmService llmService)
    {
        _chatMemoryService = chatMemoryService;
        _messageService = messageService;
        _llmService = llmService;
    }
    
    
    public async Task GenerateChatSummaryAsync(SummarizeChatJob job)
    {
        Console.WriteLine("updating summary!");
        var chatMemory = await _chatMemoryService.GetChatMemory(job.ChatId);

        var history = await _messageService.GetChatHistoryNewerThanAsync(job.ChatId, chatMemory.LastSummarizedAt, job.SummaryMessagesBatch);
        if (history.Count < SummarizeBatchSize)
            return; 

        var toSummarize = history.Take(SummarizeBatchSize / 2).ToList();

        try
        {
            await _chatMemoryService.UpdateAsync(chatMemory, toSummarize);
        }
        finally
        {
            if (chatMemory.IsSummarizing)
            {
                chatMemory.IsSummarizing = false;
                await _chatMemoryService.SaveAsync(chatMemory);
            }
        }
    }
}
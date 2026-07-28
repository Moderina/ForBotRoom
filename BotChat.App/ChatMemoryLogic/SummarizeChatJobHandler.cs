using BotChat.App.MessageLogic;

namespace BotChat.App.ChatMemoryLogic;

public class SummarizeChatJobHandler : IJobHandler<SummarizeChatJob>
{
    private const int SummarizeBatchSize = 40;
    
    private readonly IChatMemoryService _chatMemoryService;
    private readonly IMessageService _messageService;

    public SummarizeChatJobHandler(IChatMemoryService chatMemoryService, IMessageService messageService)
    {
        _chatMemoryService = chatMemoryService;
        _messageService = messageService;
    }
    
    
    public async Task HandleAsync(SummarizeChatJob job, CancellationToken cancellationToken)
    {
        Console.WriteLine("updating summary!");
        var chatMemory = await _chatMemoryService.GetChatMemory(job.ChatId);

        var history = await _messageService.GetChatHistoryNewerThanAsync(job.ChatId, chatMemory.LastSummarizedAt);
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
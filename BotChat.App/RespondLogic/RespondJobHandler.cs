using System.Text.RegularExpressions;
using BotChat.App.BotLogic;
using BotChat.App.ChatLogic;
using BotChat.App.ChatMemoryLogic;
using BotChat.App.LlmLogic;
using BotChat.Domain.Bots;

namespace BotChat.App.RespondLogic;

public class RespondJobHandler : IJobHandler<RespondJob>
{
    private const int SummarizeBatchSize = 40;
    
    private readonly IBotService _botService;
    private readonly IMessageService _messageService;
    private readonly IChatMemberRepository _chatMemberRepository;
    private readonly IChatMemoryService _chatMemoryService;
    private readonly IJobQueue<SummarizeChatJob> _jobQueue;
    private readonly ILlmService _llmService;
    private readonly IChatNotifier _chatNotifier;
    
    public RespondJobHandler(IBotService botService, IMessageService messageService, IChatMemberRepository chatMemberRepository, IChatMemoryService chatMemoryService, IJobQueue<SummarizeChatJob> jobQueue, ILlmService llmService, IChatNotifier chatNotifier)
    {
        _botService = botService;
        _messageService = messageService;
        _chatMemberRepository = chatMemberRepository;
        _chatMemoryService = chatMemoryService;
        _jobQueue = jobQueue;
        _llmService = llmService;
        _chatNotifier = chatNotifier;
    }
    
    public async Task HandleAsync(RespondJob job, CancellationToken cancellationToken)
    {
        var bot = await ChooseBotToRespond(job.ChatId);
        Console.WriteLine($"Bots name: ${bot.User.Name}");
        var chatMemory = await _chatMemoryService.GetChatMemory(job.ChatId);
        var history = await _messageService.GetChatHistoryNewerThanAsync(job.ChatId, chatMemory.LastSummarizedAt, SummarizeBatchSize);
        Console.WriteLine("message count: " + history.Count);
        if (history.Count == SummarizeBatchSize && !chatMemory.IsSummarizing)
        {
            await _jobQueue.QueueAsync(new SummarizeChatJob(job.ChatId, SummarizeBatchSize));
            // await _summarizationQueue.QueueAsync(new SummarizeChatJob(job.ChatId, SummarizeBatchSize));
        }
        //TODO: nothification for null bot
        Console.WriteLine(bot);
        if (bot == null) return;
        var members = await GetChatMemberNames(job.ChatId);
        var response = await _llmService.GenerateAsyncTEST(PromptBuilder.Build(bot, history, chatMemory.Summary, members));
        if (response == null) return;
        var humanMembers = await _chatMemberRepository.GetHumanParticipantsAsync(job.ChatId);
        var userIds = humanMembers.Select(m => m.UserId).ToList();
        foreach (var text in ParseResponse(response))
        {
            var messageDto = await _messageService.CreateMessageAsync(job.ChatId, bot.UserId, text);
            await _chatNotifier.SendMessageAsync(messageDto, userIds);
        }
    }

    private async Task<Bot> ChooseBotToRespond(Guid chatId)
    {
        var members = await _chatMemberRepository.GetBotParticipantsAsync(chatId);
        Console.WriteLine($"Found {members.Count} bot participants");
        var membersNames = members.Select(m => m.User.Name).ToList();  
        var responder = members.FirstOrDefault();
        var bot = await _botService.GetBotByIdAsync(responder.UserId);
        return bot;
    }

    private async Task<List<string>> GetChatMemberNames(Guid chatId)
    {
        var members = await _chatMemberRepository.GetAllParticipantsAsync(chatId);
        Console.WriteLine($"Found {members.Count} participants");
        var membersNames = members.Select(m => m.User.Name).ToList();  
        return membersNames;
    }

    private string[] ParseResponse(string response)
    {
        var messages = Regex.Matches(response.Trim(), @"<message>(.*?)</message>", RegexOptions.Singleline)
            .Select(m => m.Groups[1].Value.Trim())
            .Where(s => s.Length > 0)
            .ToList();
        if (messages.Count == 0)
        {
            messages = response.Trim()
                .Split(new[] { ". ", "! ", "? " }, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Trim())
                .ToList();
        }
        return messages.ToArray();
    }
}
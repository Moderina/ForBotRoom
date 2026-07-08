using BotChat.App.BotLogic;
using BotChat.App.ChatLogic;
using BotChat.App.LlmLogic;

namespace BotChat.App.ConversationLogic;

public class ConversationService : IConversationService
{
    private readonly IBotService _botService;
    private readonly IMessageService _messageService;
    private readonly IChatMemberRepository _chatMemberRepository;
    private readonly ILlmService _llmService;
    private readonly IChatNotifier _chatNotifier;

    public ConversationService(
        IBotService botService, 
        IMessageService messageService, 
        IChatMemberRepository chatMemberRepository, 
        ILlmService llmService,
        IChatNotifier chatNotifier)
    {
        _botService = botService;
        _messageService = messageService;
        _chatMemberRepository = chatMemberRepository;
        _llmService = llmService;
        _chatNotifier = chatNotifier;
    }
    
    public async Task GenerateBotResponseAsync(ConversationJob job)
    {
        //add message to bot memory
        var members = await _chatMemberRepository.GetBotParticipantsAsync(job.ChatId);
        Console.WriteLine($"Found {members.Count} participants");
        var responder = members[0];
        Console.WriteLine(responder.UserId);
        var bot = await _botService.GetBotByIdAsync(responder.UserId);
        Console.WriteLine($"Bots name: ${bot.User.Name}");
        var history = await _messageService.GetChatHistoryAsync(job.ChatId);
        
        //TODO: nothification for null bot
        Console.WriteLine(bot);
        if (bot == null) return;
        var response = await _llmService.GenerateAsync(PromptBuilder.BuildPrompt_Respond(bot, history));
        if (response == null) return;
        Console.WriteLine(response.Response);
        var messageDto = await _messageService.CreateMessageAsync(job.ChatId, bot.UserId, response.Response);
        await _chatNotifier.SendMessageAsync(messageDto);
    }

}
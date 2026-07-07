using BotChat.App.BotLogic;
using BotChat.App.ChatLogic;
using BotChat.App.LlmLogic;
using BotChat.Contracts.Bots;
using BotChat.Contracts.Chat;
using BotChat.Domain.Bots;
using BotChat.Domain.Chats;

namespace BotChat.App.ConversationLogic;

public class ConversationService : IConversationService
{
    private readonly IBotService _botService;
    private readonly IMessageService _messageService;
    private readonly IChatService _chatService;
    private readonly IChatMemberRepository _chatMemberRepository;
    private readonly ILlmService _llmService;

    public ConversationService(IBotService botService, IMessageService messageService, IChatService chatService, IChatMemberRepository chatMemberRepository, ILlmService llmService)
    {
        _botService = botService;
        _messageService = messageService;
        _chatService = chatService;
        _chatMemberRepository = chatMemberRepository;
        _llmService = llmService;
    }
    
    public async Task<MessageDto> HandleUserMessageAsync(Guid chatId, Guid userId, string message)
    {
        Console.WriteLine($"Received message from {userId}: {message}");
        var messageDto = await _messageService.CreateMessageAsync(chatId, userId, message);
        //add message to bot memory
        var members = await _chatMemberRepository.GetBotParticipantsAsync(chatId);
        Console.WriteLine($"Found {members.Count} participants");
        var responder = members[0];
        Console.WriteLine(responder.UserId);
        var bot = await _botService.GetBotByIdAsync(responder.UserId);
        Console.WriteLine($"Bots name: ${bot.User.Name}");
        var history = await _messageService.GetChatHistoryAsync(chatId);
        
        //TODO: nothification for null bot
        Console.WriteLine(bot);
        if (bot == null) return messageDto;
        var response = await _llmService.GenerateAsync(PromptBuilder.BuildPrompt_Respond(bot, history));
        if (response == null) return messageDto;
        Console.WriteLine(response.Response);
        messageDto = await _messageService.CreateMessageAsync(chatId, bot.UserId, response.Response);
        return messageDto;
    }

}
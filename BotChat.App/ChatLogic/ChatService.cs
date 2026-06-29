using BotChat.Contracts.Chat;
using BotChat.Domain.Chats;

namespace BotChat.App.ChatLogic;

public class ChatService : IChatService
{
    private IChatRepository _chatRepository;
    private IChatMemberRepository _chatMemberRepository;

    public ChatService(IChatRepository chatRepository, IChatMemberRepository chatMemberRepository)
    {
        _chatRepository = chatRepository;
        _chatMemberRepository = chatMemberRepository;
    }
    
    public async Task<List<ChatDto>> GetActiveChatsAsync()
    {
        var chats = await _chatRepository.GetActiveChatsAsync();
        var chatDtos = new List<ChatDto>();
        foreach (var chat in chats)
        {
            chatDtos.Add(new ChatDto()
            {
                Id = chat.Id,
                Name = chat.Name,
            });
        }
        return chatDtos;
    }

    public async Task<ChatDto> CreateChatAsync(Guid userId, CreateChatRequest request)
    {
        var chat = new Chat(request.Name);
        await _chatRepository.AddChatAsync(chat);
        var member = new ChatMember() { UserId = userId, ChatId = chat.Id };
        await _chatMemberRepository.AddParticipantAsync(member);
        var member2 = new ChatMember() { UserId = request.BotId, ChatId = chat.Id };
        await _chatMemberRepository.AddParticipantAsync(member2);
        
        var chatdto = new ChatDto()
        {
            Id = chat.Id,
            Name = chat.Name,
        };
        return chatdto;
    }

    public async Task DisableChatAsync(Guid chatId)
    {
        var result = await _chatRepository.DisableChatAsync(chatId);
    }

    public Task DeleteChatAsync(Guid chatId)
    {
        return Task.CompletedTask;
    }
}
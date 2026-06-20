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
    
    public List<ChatDto> GetChats()
    {
        var chats = _chatRepository.GetChatsAsync().Result;
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

    public ChatDto CreateChat(Guid userId, CreateChatRequest request)
    {
        var chat = new Chat(request.Name);
        var member = new ChatMember() { UserId = userId, ChatId = chat.Id };
        _chatRepository.AddChatAsync(chat);
        _chatMemberRepository.AddParticipantAsync(member);
        
        var chatdto = new ChatDto()
        {
            Id = chat.Id,
            Name = chat.Name,
        };
        return chatdto;
    }

    public Task DeleteChat(Guid chatId)
    {
        return Task.CompletedTask;
    }
}
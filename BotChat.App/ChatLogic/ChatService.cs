using BotChat.Contracts.Chat;
using BotChat.Domain;
using BotChat.Domain.Chats;
using BotChat.Domain.Users;

namespace BotChat.App.ChatLogic;

public class ChatService : IChatService
{
    private IChatRepository _chatRepository;
    private IChatMemberRepository _chatMemberRepository;
    private IMessageRepository _messageRepository;

    public ChatService(IChatRepository chatRepository, IChatMemberRepository chatMemberRepository, IMessageRepository messageRepository)
    {
        _chatRepository = chatRepository;
        _chatMemberRepository = chatMemberRepository;
        _messageRepository = messageRepository;
    }
    
    public async Task<List<ChatListItemDto>> GetActiveChatsAsync()
    {
        var chats = await _chatRepository.GetActiveChatsAsync();
        var chatDtos = new List<ChatListItemDto>();
        foreach (var chat in chats)
        {
            chatDtos.Add(new ChatListItemDto()
            {
                Id = chat.Id,
                Name = chat.Name,
            });
        }
        return chatDtos;
    }

    public async Task<List<User>> GetHumanMembersOfChatAsync(Guid chatId)
    {
        var members = await _chatMemberRepository.GetHumanParticipantsAsync(chatId);
        var users = new List<User>();
        foreach (var mem in members)
        {
            users.Add(mem.User);
        }
        return users;
    }

    public async Task<ChatDetailsDto> GetChatDetailsAsync(Guid chatId)
    {
        var chat = await _chatRepository.GetChatAsync(chatId);
        var members = await _chatMemberRepository.GetAllParticipantsAsync(chatId);
        var messages = await _messageRepository.GetChatHistoryAsync(chatId, 20, DateTime.Now);
        
        var memberDtos = new List<ChatMemberDto>();
        foreach (var member in members)
        {
            var user = member.User;
            var imageUrl = string.IsNullOrEmpty(user.ProfilePictureUrl)
                ? "/assets/images/default-profile.jpg"
                : $"/api/media/profile-pictures/{user.ProfilePictureUrl}";
            
            memberDtos.Add(new ChatMemberDto()
            {
                UserId = user.Id,
                Name = user.Name,
                ProfilePictureUrl = imageUrl,
                Type = user.Type == UserType.Bot ? "bot" : "user",
            });
        }
        
        var messageDtos = new List<MessageDto>();
        foreach (var message in messages)
        {
            messageDtos.Add(new MessageDto()
            {
                Id = message.Id,
                ChatId = message.ChatId,
                AuthorId = message.AuthorId,
                UserType = message.Author.Type == UserType.Bot ? "bot" : "user",
                Content = message.Content,
                Timestamp = message.Timestamp,
            });
        }

        var dto = new ChatDetailsDto()
        {
            Id = chat.Id,
            Name = chat.Name,
            Members = memberDtos,
            Messages = messageDtos,
        };
        return dto;
    }

    public async Task<ChatDetailsDto> CreateChatAsync(Guid userId, CreateChatRequest request)
    {
        var chat = new Chat(request.Name);
        await _chatRepository.AddChatAsync(chat);
        var member = new ChatMember() { UserId = userId, ChatId = chat.Id };
        await _chatMemberRepository.AddParticipantAsync(member);
        var member2 = new ChatMember() { UserId = request.BotId, ChatId = chat.Id };
        await _chatMemberRepository.AddParticipantAsync(member2);
        
        return await GetChatDetailsAsync(chat.Id);
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
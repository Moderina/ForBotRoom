using BotChat.Domain.Chats;

namespace BotChat.App.ChatLogic;

public interface IChatMemberRepository
{
    public Task<List<ChatMember>> GetBotParticipantsAsync(Guid chatid);
    public Task<List<ChatMember>> GetHumanParticipantsAsync(Guid chatid);
    public Task<List<ChatMember>> GetAllParticipantsAsync(Guid chatId);
    public Task AddParticipantAsync(ChatMember member);
    public Task DeleteParticipantAsync(ChatMember member);
}
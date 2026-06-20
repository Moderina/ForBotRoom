using BotChat.Domain.Chats;

namespace BotChat.App.ChatLogic;

public interface IChatMemberRepository
{
    public Task<List<ChatMember>> GetParticipantsAsync(Guid chatid);
    public Task AddParticipantAsync(ChatMember member);
    public Task DeleteParticipantAsync(ChatMember member);
}
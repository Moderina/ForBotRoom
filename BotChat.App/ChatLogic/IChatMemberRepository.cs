using BotChat.Domain.Chats;

namespace BotChat.App.ChatLogic;

public interface IChatMemberRepository
{
    public Task<List<ChatMember>> GetBotParticipantsAsync(Guid chatid);
    public Task AddParticipantAsync(ChatMember member);
    public Task DeleteParticipantAsync(ChatMember member);
}
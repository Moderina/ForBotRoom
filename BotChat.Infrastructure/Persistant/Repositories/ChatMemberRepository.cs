using BotChat.App.ChatLogic;
using BotChat.Domain.Chats;
using Microsoft.EntityFrameworkCore;

namespace BotChat.Infrastructure.Persistant.Repositories;

public class ChatMemberRepository : IChatMemberRepository
{
    private readonly AppDbContext _db;
    
    public ChatMemberRepository(AppDbContext db)
    {
        _db = db;
    }
    public Task<List<ChatMember>> GetParticipantsAsync(Guid chatId)
    {
        return _db.ChatMembers.Where(c => c.ChatId == chatId).ToListAsync();
    }

    public Task AddParticipantAsync(ChatMember member)
    {
        _db.ChatMembers.Add(member);
        return _db.SaveChangesAsync();
    }

    public Task DeleteParticipantAsync(ChatMember member)
    {
        _db.ChatMembers.Remove(member);
        return _db.SaveChangesAsync();
    }
}
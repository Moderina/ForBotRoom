using BotChat.Domain.Chats;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BotChat.Infrastructure.Persistant.Configs;

public class ChatMemberConfiguration : IEntityTypeConfiguration<ChatMember>
{
    public void Configure(EntityTypeBuilder<ChatMember> builder)
    {
        builder.HasKey(x => new { x.ChatId, x.UserId });

        builder.HasOne(x => x.Chat)
            .WithMany(x => x.Participants)
            .HasForeignKey(x => x.ChatId);

        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId);
    }
}
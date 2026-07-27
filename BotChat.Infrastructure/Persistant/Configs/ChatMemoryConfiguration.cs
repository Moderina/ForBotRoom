using BotChat.Domain.Chats;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BotChat.Infrastructure.Persistant.Configs;

public class ChatMemoryConfiguration : IEntityTypeConfiguration<ChatMemory>
{
    public void Configure(EntityTypeBuilder<ChatMemory> builder)
    {
        builder.HasKey(x => x.ChatId);
        
        builder.Property(x => x.ChatId)
            .ValueGeneratedNever();
        
        builder.ToTable("ChatMemories");
    }
}
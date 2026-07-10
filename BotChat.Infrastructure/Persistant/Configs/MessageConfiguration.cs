using BotChat.Domain;
using BotChat.Domain.Chats;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BotChat.Infrastructure.Configs;

public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.AuthorId)
            .IsRequired();
        
        builder.Property(x => x.ChatId)
            .IsRequired();
        
        builder.Property(x => x.Content)
            .HasMaxLength(500)
            .IsRequired();

        builder.HasOne<Chat>()
            .WithMany()
            .HasForeignKey(x => x.ChatId);

        builder.HasOne<User>(x => x.Author)
            .WithMany()
            .HasForeignKey(x => x.AuthorId);

        builder.ToTable("Messages");
    }
}
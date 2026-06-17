using System.Text.Json;
using BotChat.Domain;
using BotChat.Domain.Agents;
using BotChat.Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BotChat.Infrastructure.Configs;

public class BotConfiguration : IEntityTypeConfiguration<Bot>
{
    public void Configure(EntityTypeBuilder<Bot> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.PersonalityData).HasConversion(
            v => JsonSerializer.Serialize(v, JsonHelper.CamelCase),
            v => JsonSerializer.Deserialize<PersonalityData>(v, JsonHelper.CamelCase)!
        );
        
        builder.Ignore(x => x.Mood);

        builder.HasOne<User>()
            .WithOne()
            .HasForeignKey<Bot>(b => b.UserId);

        builder.ToTable("Bots");
    }
}
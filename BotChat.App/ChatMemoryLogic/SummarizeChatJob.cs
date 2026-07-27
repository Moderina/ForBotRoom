namespace BotChat.App.ChatMemoryLogic;

public record SummarizeChatJob (
    Guid ChatId,
    int SummarizeBatchSize
);
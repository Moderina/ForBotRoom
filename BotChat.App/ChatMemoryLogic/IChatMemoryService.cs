using BotChat.Domain.Chats;

namespace BotChat.App.ChatMemoryLogic;

public interface IChatMemoryService
{
    public Task<ChatMemory> GetChatMemory(Guid chatId);
    public Task<string?> GetSummaryForPromptAsync(Guid chatId);
    public Task<ChatMemory> UpdateAsync(ChatMemory memory, List<Message> messages);
    public Task SaveAsync(ChatMemory memory);
}
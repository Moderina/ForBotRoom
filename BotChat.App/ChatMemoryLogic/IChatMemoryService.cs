using BotChat.Domain.Chats;

namespace BotChat.App.ConversationLogic;

public interface IChatMemoryService
{
    public Task<ChatMemory> GetChatMemory(Guid chatId);
    public Task<string?> GetSummaryForPromptAsync(Guid chatId);
    public Task<ChatMemory> UpdateMemoryAsync(ChatMemory memory, List<Message> messages);
}
namespace BotChat.Domain.Chats;

public class Chat
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    
    public ICollection<ChatMember> Participants { get; set; } = [];
    
    public bool Disabled { get; set; } = false;

    public Chat() {}

    public Chat(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
    }
}
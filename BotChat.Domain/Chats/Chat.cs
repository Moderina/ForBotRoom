using BotChat.Domain.Agents;

namespace BotChat.Domain.Chats;

public class Chat
{
    public int Id { get; }
    public string Name { get; } = "";
    public Bot Bot { get; }
    
    public bool Disabled { get; set; } = false;

    public Chat() {}

    public Chat(int id, string name, Bot bot)
    {
        Id = id;
        Name = name;
        Bot = bot;
    }
}
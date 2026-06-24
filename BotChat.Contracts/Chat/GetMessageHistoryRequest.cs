namespace BotChat.Contracts.Chat;

public class GetMessageHistoryRequest
{
    public int? Amount { get; set; }
    public long? Before { get; set; }
}
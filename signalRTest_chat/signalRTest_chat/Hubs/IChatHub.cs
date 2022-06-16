namespace signalRTest_chat.Hubs
{
    public interface IChatHub
    {
        Task SendMessage(string text);
        Task CreateGroup(string groupName);
    }
}
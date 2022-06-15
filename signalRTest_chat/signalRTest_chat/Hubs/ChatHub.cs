using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace signalRTest_chat.Hubs
{
    public class ChatHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(string text)
        {
            //todo presitent in db
            var userName = Context.User.FindFirstValue(ClaimTypes.Name);
            await Clients.All.SendAsync("ReceiveMessage", $"{userName}: {text}");
        }
    }
}

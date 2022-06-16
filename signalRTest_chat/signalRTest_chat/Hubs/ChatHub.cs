using CoreLayer.Services.Chats.ChatGroups;
using CoreLayer.Utilities;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace signalRTest_chat.Hubs
{
    public class ChatHub : Hub, IChatHub
    {
        public IChatGroupService _chatGroupService { get; set; }
        public ChatHub(IChatGroupService chatGroupService)
        {
            _chatGroupService = chatGroupService;
        }
        public async Task CreateGroup(string groupName)
        {
            try {
               var group = await _chatGroupService.InsertGroupsAsync(groupName, Context.User.GetUserId());
               await Clients.Caller.SendAsync("NewGroup", groupName, group.GroupToken);
            }
            catch {
                await Clients.Caller.SendAsync("NewGroup", "Error");
            }
        }

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

using CoreLayer.Services.Chats;
using CoreLayer.Services.Chats.ChatGroups;
using CoreLayer.Services.Users.UserGroups;
using CoreLayer.Utilities;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace signalRTest_chat.Hubs
{
    public class ChatHub : Hub, IChatHub
    {
        public IChatGroupService _chatGroupService { get; set; }
        public IChatService _chatService { get; set; }
        public IUserGroupService _userGroupService { get; set; }
        public ChatHub(IChatGroupService chatGroupService, IChatService chatService, IUserGroupService userGroupService)
        {
            _chatGroupService = chatGroupService;
            _userGroupService = userGroupService;
            _chatService = chatService;
        }
        public async Task CreateGroup(string groupName)
        {
            try
            {
                var group = await _chatGroupService.InsertGroupsAsync(
                    new CoreLayer.ViewModels.Chats.CreateGroupViewModel()
                    {
                        GroupName = groupName,
                        UserId = Context.User.GetUserId()
                    });
                await Clients.Caller.SendAsync("NewGroup", groupName, group.GroupToken);
            }
            catch
            {
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

        public async Task JoinGroup(string token)
        {
            var group = await _chatGroupService.GetGroupBy(token);
            if (group == null) await Clients.Caller.SendAsync("Error", "Group Not Found");
            else
            {
                if (!await _userGroupService.IsUserInGroup(Context.User.GetUserId(), token))
                {
                    await _userGroupService.JoinGroup(Context.User.GetUserId(), group.Id);
                    await Clients.Caller.SendAsync("NewGroup", group.GroupTitle, group.GroupToken, group.ImageName);
                }

                await Groups.AddToGroupAsync(Context.ConnectionId, group.Id.ToString());
                await Clients.Group(group.Id.ToString()).SendAsync("JoinGroup", group, group.Chats);
            }
        }
    }
}

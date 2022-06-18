using CoreLayer.Services.Chats.ChatGroups;
using CoreLayer.Services.Users.UserGroups;
using CoreLayer.Utilities;
using CoreLayer.ViewModels.Chats;
using DataLayer.Entities.Chats;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using signalRTest_chat.Hubs;
using signalRTest_chat.Models;
using System.Diagnostics;

namespace signalRTest_chat.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserGroupService _userGroupService;
        private readonly IChatGroupService _chatGroupService;
        private readonly IHubContext<ChatHub> _chatHub;
        public HomeController(ILogger<HomeController> logger, IChatGroupService chatGroup, IHubContext<ChatHub> chatHub, IUserGroupService userGroupService)
        {
            _logger = logger;
            _chatGroupService = chatGroup;
            _chatHub = chatHub;
            _userGroupService = userGroupService;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var model = await _userGroupService.GetUserGroupsAsync(User.GetUserId());
            return View(model);
        }
        [Authorize]
        [HttpPost]
        public async Task CreateGroup([FromForm] CreateGroupViewModel model) {
            try
            {
                model.UserId = User.GetUserId();
                var group = await _chatGroupService.InsertGroupsAsync(model);
                await _chatHub.Clients.User(User.GetUserId().ToString()).SendAsync("NewGroup", group.GroupTitle, group.GroupToken, group.ImageName);
            }
            catch (Exception)
            {
                await _chatHub.Clients.User(User.GetUserId().ToString()).SendAsync("NewGroup", "ERROR");
                throw;
            }
        }

        public async Task<IActionResult> Search(string title) {
            return new ObjectResult(await _chatGroupService.Search(title));
        }
    }
}
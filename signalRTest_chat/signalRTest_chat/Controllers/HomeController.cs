using CoreLayer.Services.Chats.ChatGroups;
using CoreLayer.Utilities;
using DataLayer.Entities.Chats;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using signalRTest_chat.Models;
using System.Diagnostics;

namespace signalRTest_chat.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IChatGroupService _chatGroupService;

        public HomeController(ILogger<HomeController> logger, IChatGroupService chatGroup)
        {
            _logger = logger;
            _chatGroupService = chatGroup;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var model = await _chatGroupService.GetChatGroupsAsync(User.GetUserId());
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
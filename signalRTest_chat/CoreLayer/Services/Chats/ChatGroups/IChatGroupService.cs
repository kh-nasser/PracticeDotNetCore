using CoreLayer.ViewModels.Chats;
using DataLayer.Entities.Chats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Services.Chats.ChatGroups
{
    public interface IChatGroupService
    {
        Task<List<ChatGroup>> GetChatGroupsAsync(long userId);
        Task<ChatGroup> InsertGroupsAsync(CreateGroupViewModel model);

    }
}

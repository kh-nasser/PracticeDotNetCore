using CoreLayer.ViewModels.Chats;
using DataLayer.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Services.Users.UserGroups
{
    public interface IUserGroupService
    {
        Task<List<UserGroupViewModel>> GetUserGroupsAsync(long userId);
        Task JoinGroup(long userId, long groupId);
        Task<bool> IsUserInGroup(long userId, long groupId);
        Task<bool> IsUserInGroup(long userId, string token);
    }
}

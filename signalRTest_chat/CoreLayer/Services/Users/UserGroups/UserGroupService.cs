using CoreLayer.ViewModels.Chats;
using DataLayer.Context;
using DataLayer.Entities.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Services.Users.UserGroups
{
    public class UserGroupService : BaseService, IUserGroupService
    {
        public UserGroupService(ChatContext context) : base(context)
        {
        }

        public async Task<List<UserGroupViewModel>> GetUserGroupsAsync(long userId)
        {
            var result = Table<UserGroup>().Include(c => c.Group.Chats).Where(g => g.UserId == userId).Select(s => new UserGroupViewModel()
            {
                ImageName = s.Group.ImageName,
                GroupName = s.Group.GroupTitle,
                LastChat = s.Group.Chats.OrderBy(d => d.CreateDate).First(),
                Token = s.Group.GroupToken
            });

            return await result.ToListAsync();

        }

        public async Task JoinGroup(long userId, long groupId)
        {
            var model = new UserGroup()
            {
                CreateDate = DateTime.Now,
                GroupId = groupId,
                UserId = userId
            };
            Insert(model);
            await Save();
        }
    }
}

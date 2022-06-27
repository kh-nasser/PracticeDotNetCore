using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CoreLayer.Services.Users.UserGroups;
using CoreLayer.Utilities;
using CoreLayer.ViewModels.Chats;
using DataLayer.Context;
using DataLayer.Entities.Chats;
using DataLayer.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace CoreLayer.Services.Chats.ChatGroups
{
    public class ChatGroupService : BaseService, IChatGroupService
    {
        private IUserGroupService _Usergroup;

        public ChatGroupService(EchatContext context, IUserGroupService usergroup) : base(context)
        {
            _Usergroup = usergroup;
        }

        public async Task<List<SearchResultViewModel>> Search(string title, long userId)
        {
            var result = new List<SearchResultViewModel>();
            if (string.IsNullOrWhiteSpace(title))
                return result;

            var groups = await Table<ChatGroup>()
                .Where(g => g.GroupTitle.Contains(title) && !g.IsPrivate)
                .Select(s => new SearchResultViewModel()
                {
                    ImageName = s.ImageName,
                    Token = s.GroupToken,
                    IsUser = false,
                    Title = s.GroupTitle
                }).ToListAsync();

            var users = await Table<User>()
                .Where(g => g.UserName.Contains(title) && g.Id != userId)
                .Select(s => new SearchResultViewModel()
                {
                    ImageName = s.Avatar,
                    Token = s.Id.ToString(),
                    IsUser = true,
                    Title = s.UserName
                }).ToListAsync();
            result.AddRange(groups);
            result.AddRange(users);
            return result;
        }

        public async Task<List<ChatGroup>> GetUserGroups(long userId)
        {
            return await Table<ChatGroup>()
                .Include(c => c.Chats)
                .Where(g => g.OwnerId == userId)
                .OrderByDescending(d => d.CreateDate).ToListAsync();
        }

        public async Task<ChatGroup> InsertGroup(CreateGroupViewModel model)
        {
            if (model.ImageFile == null || !FileValidation.IsValidImageFile(model.ImageFile.FileName))
                throw new Exception();


            var imageName = await model.ImageFile.SaveFile("wwwroot/image/groups");

            var chatGroup = new ChatGroup()
            {
                CreateDate = DateTime.Now,
                GroupTitle = model.GroupName,
                OwnerId = model.UserId,
                GroupToken = Guid.NewGuid().ToString(),
                ImageName = imageName
            };
            Insert(chatGroup);
            await Save();
            await _Usergroup.JoinGroup(model.UserId, chatGroup.Id);
            return chatGroup;
        }

        public async Task<ChatGroup> InsertPrivateGroup(long userId, long receiverId)
        {
            var group = await Table<ChatGroup>()
                .Include(c => c.User)
                .Include(c => c.Receiver)
                .SingleOrDefaultAsync(s => 
                    (s.OwnerId == userId && s.ReceiverId == receiverId)
                    || (s.OwnerId == receiverId && s.ReceiverId == userId));

            if (group == null)
            {
                var groupCreated = new ChatGroup()
                {
                    CreateDate = DateTime.Now,
                    GroupTitle = $"Chat With {receiverId}",
                    GroupToken = Guid.NewGuid().ToString(),
                    ImageName = "Default.jpg",
                    IsPrivate = true,
                    OwnerId = userId,
                    ReceiverId = receiverId
                };
                Insert(groupCreated);
                await Save();
                return await GetGroupBy(groupCreated.Id);
            }
            return group;
        }

        public async Task<ChatGroup> GetGroupBy(long id)
        {
            return await  Table<ChatGroup>()
                .Include(c => c.User)
                .Include(c => c.Receiver)
                .FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<ChatGroup> GetGroupBy(string token)
        {
            return await Table<ChatGroup>()
                .Include(c => c.User)
                .Include(c => c.Receiver)
                .FirstOrDefaultAsync(g => g.GroupToken == token);
        }
    }
}
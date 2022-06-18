﻿using CoreLayer.Services.Users.UserGroups;
using CoreLayer.Utilities;
using CoreLayer.ViewModels.Chats;
using DataLayer.Context;
using DataLayer.Entities.Chats;
using DataLayer.Entities.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Services.Chats.ChatGroups
{
    public class ChatGroupService : BaseService, IChatGroupService
    {
        private IUserGroupService _userGroupService;
        public ChatGroupService(ChatContext context, IUserGroupService userGroup) : base(context)
        {
            _userGroupService = userGroup;
        }

        public async Task<List<ChatGroup>> GetChatGroupsAsync(long userId)
        {
            return await Table<ChatGroup>().Include(c => c.Chats).Where(g => g.OwnerId == userId).OrderByDescending(d => d.CreateDate).ToListAsync();
        }

        public async Task<ChatGroup> GetGroupBy(string token)
        {
            return await Table<ChatGroup>().Include(x=>x.Chats).FirstOrDefaultAsync(g => g.GroupToken == token);
        }

        public async Task<ChatGroup> GetGroupBy(long id) 
        {
            return await GetById<ChatGroup>(id);
        }

        public async Task<ChatGroup> InsertGroupsAsync(CreateGroupViewModel model)
        {
            if (model.ImageFile == null || !FileValidation.IsValidImageFile(model.ImageFile.FileName)) throw new ArgumentException();

            var imageName = await model.ImageFile.SaveFile("wwwroot/image/group");
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
            await _userGroupService.JoinGroup(model.UserId, chatGroup.Id);

            return chatGroup;
        }

        public async Task<List<SearchResultViewModel>> Search(string title)
        {
            var result = new List<SearchResultViewModel>();
            if(string.IsNullOrWhiteSpace(title)) return result;

            var groups = await Table<ChatGroup>().Where(g => g.GroupTitle.Contains(title))
                .Select(c => new SearchResultViewModel()
                {
                    ImageName = c.ImageName,
                    Token = c.GroupToken,
                    Title = c.GroupTitle,
                    IsUser = false
                }).ToListAsync();

            var users = await Table<User>().Where(g => g.UserName.Contains(title))
                .Select(c => new SearchResultViewModel()
                {
                    ImageName = c.Avatar,
                    Token = c.Id.ToString(),
                    Title = c.UserName,
                    IsUser = true
                }).ToListAsync();

            result.AddRange(groups);
            result.AddRange(users);
            return result;
        }
    }
}

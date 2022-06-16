using DataLayer.Context;
using DataLayer.Entities.Chats;
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
        public ChatGroupService(ChatContext context) : base(context)
        {
        }

        public async Task<List<ChatGroup>> GetChatGroupsAsync(long userId)
        {
            return await Table<ChatGroup>().Include(c => c.Chats).Where(g => g.OwnerId == userId).OrderByDescending(d => d.CreateDate).ToListAsync();
        }

        public async Task<ChatGroup> InsertGroupsAsync(string groupName, long userId)
        {
            var chatGroup = new ChatGroup()
            {
                CreateDate = DateTime.Now,
                GroupTitle = groupName,
                OwnerId = userId,
                GroupToken = Guid.NewGuid().ToString(),
            };

            Insert(chatGroup);
            await Save();

            return chatGroup;
        }
    }
}

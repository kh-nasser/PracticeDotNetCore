using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreLayer.Utilities;
using CoreLayer.ViewModels.Chats;
using DataLayer.Context;
using DataLayer.Entities.Chats;
using Microsoft.EntityFrameworkCore;

namespace CoreLayer.Services.Chats
{
    public class ChatService : BaseService, IChatService
    {
        public ChatService(EchatContext context) : base(context)
        {
        }

        public async Task<ChatViewModel> SendMessage(InsertChatVIewModel chat)
        {
            var group = await GetById<ChatGroup>(chat.GroupId);
            var chatModel = new Chat()
            {
                CreateDate = DateTime.Now,
                GroupId = chat.GroupId,
                UserId = chat.UserId
            };
            if (chat.FileAttach != null)
            {
                var fileName = await chat.FileAttach.SaveFile("wwwroot/files/");
                chatModel.ChatBody = chat.FileAttach.FileName;
                chatModel.FileAttach = fileName;
                Insert(chatModel);
                await Save();
                return new ChatViewModel()
                {
                    UserName = " ",
                    CreateDate = $"{chatModel.CreateDate.Hour}:{chatModel.CreateDate.Minute}",
                    ChatBody = chatModel.ChatBody,
                    GroupName = group.GroupTitle,
                    GroupId = group.Id,
                    UserId = chat.UserId,
                    FileAttach = fileName
                };
            }
            chatModel.ChatBody = chat.ChatBody;
            Insert(chatModel);
            await Save();
            return new ChatViewModel()
            {
                UserName = " ",
                CreateDate = $"{chatModel.CreateDate.Hour}:{chatModel.CreateDate.Minute}",
                ChatBody = chatModel.ChatBody,
                GroupName = group.GroupTitle,
                GroupId = group.Id,
                UserId = chat.UserId
            };
        }

        public async Task<List<ChatViewModel>> GetChatGroup(long groupId)
        {
            return await Table<Chat>()
                .Include(c => c.User)
                .Include(c => c.CharGroup)
                .Where(g => g.GroupId == groupId)
                .Select(s => new ChatViewModel()
                {
                    UserName = s.User.UserName,
                    CreateDate = $"{s.CreateDate.Hour}:{s.CreateDate.Minute}",
                    ChatBody = s.ChatBody,
                    GroupName = s.CharGroup.GroupTitle,
                    UserId = s.UserId,
                    FileAttach = s.FileAttach,
                    GroupId = s.GroupId
                }).ToListAsync();
        }
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using CoreLayer.ViewModels.Chats;
using DataLayer.Entities.Chats;

namespace CoreLayer.Services.Chats
{
    public interface IChatService
    {
        Task<ChatViewModel> SendMessage(InsertChatVIewModel chat);
        Task<List<ChatViewModel>> GetChatGroup(long groupId);
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using CoreLayer.ViewModels.Chats;
using DataLayer.Entities.Chats;

namespace CoreLayer.Services.Chats.ChatGroups
{
    public interface IChatGroupService
    {
        Task<List<SearchResultViewModel>> Search(string title,long userId);
        Task<List<ChatGroup>> GetUserGroups(long userId);
        Task<ChatGroup> InsertGroup(CreateGroupViewModel model);
        Task<ChatGroup> InsertPrivateGroup(long userId, long receiverId);
        Task<ChatGroup> GetGroupBy(long id);
        Task<ChatGroup> GetGroupBy(string token);

    }
}
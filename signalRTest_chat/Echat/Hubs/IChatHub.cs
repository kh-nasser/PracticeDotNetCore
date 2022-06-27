using System.Threading.Tasks;
using CoreLayer.ViewModels.Chats;
using Microsoft.AspNetCore.Http;

namespace Echat.Hubs
{
    public interface IChatHub
    {
        Task JoinGroup(string token, long currentGroupId);
        Task JoinPrivateGroup(long receiverId, long currentGroupId);
    }
}
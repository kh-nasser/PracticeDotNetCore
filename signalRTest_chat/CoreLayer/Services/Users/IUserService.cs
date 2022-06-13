using System.Threading.Tasks;
using CoreLayer.ViewModels.Auth;

namespace CoreLayer.Services.Users
{
    public interface IUserService
    {
        Task<bool> IsUserExist(string userName);
        Task<bool> IsUserExist(long userId);
        Task<bool> RegisterUser(RegisterViewModel registerModel);
    }
}
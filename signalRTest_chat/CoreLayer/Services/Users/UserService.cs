using System;
using System.Threading.Tasks;
using CoreLayer.Utilities.Security;
using CoreLayer.ViewModels.Auth;
using DataLayer.Context;
using DataLayer.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace CoreLayer.Services.Users
{
    public class UserService:BaseService,IUserService
    {
        public UserService(ChatContext context) : base(context)
        {
        }

        public async Task<bool> IsUserExist(string userName)
        {
            return await Table<User>().AnyAsync(u => u.UserName == userName.ToLower());
        }

        public async Task<bool> IsUserExist(long userId)
        {
            return await Table<User>().AnyAsync(u => u.Id == userId);
        }

        public async Task<bool> RegisterUser(RegisterViewModel registerModel)
        {
            if (await IsUserExist(registerModel.UserName))
                return false;

            if (registerModel.Password != registerModel.RePassword)
                return false;

            var password = registerModel.Password.EncodePasswordMd5();
            var user=new User()
            {
                Avatar = "Default.jpg",
                CreateDate = DateTime.Now,
                Password = password,
                UserName = registerModel.UserName.ToLower()
            };
            Insert(user);
            await Save();
            return true;
        }
    }
}
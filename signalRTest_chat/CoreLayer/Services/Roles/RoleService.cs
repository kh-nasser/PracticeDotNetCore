using DataLayer.Context;

namespace CoreLayer.Services.Roles
{
    public class RoleService:BaseService,IRoleService
    {
        public RoleService(ChatContext context) : base(context)
        {
        }
    }
}
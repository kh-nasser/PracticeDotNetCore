using DataLayer.Context;

namespace CoreLayer.Services.Roles
{
    public class RoleService:BaseService,IRoleService
    {
        public RoleService(EchatContext context) : base(context)
        {
        }
    }
}
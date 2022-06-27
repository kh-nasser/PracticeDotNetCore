using System.ComponentModel.DataAnnotations.Schema;
using DataLayer.Enums;

namespace DataLayer.Entities.Roles
{
    public class RolePermission:BaseEntity
    {
        public long RoleId { get; set; }
        public Permission Permission { get; set; }

        #region Relation

        [ForeignKey("RoleId")]
        public Role Role { get; set; }

        #endregion
    }
}
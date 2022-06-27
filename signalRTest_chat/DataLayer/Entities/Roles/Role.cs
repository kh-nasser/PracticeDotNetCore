using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities.Roles
{
    public class Role:BaseEntity
    {
        [MaxLength(50)]
        public string Title { get; set; }
    }
}